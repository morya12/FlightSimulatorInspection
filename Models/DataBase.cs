using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using FlightSimulatorInspection.Models;


namespace FlightSimulatorInspection.Models
{
    #region Helper Classes
    public class CorrelatedFeatures
    {
        private string feature1, feature2;  // names of the correlated features
        private float lineA, lineB;
        private float cx = 0.0f, cy = 0.0f;
        private float radius = 0.0f;

        public string Feature1 { get { return feature1; } set { feature1 = value; } }
        public string Feature2 { get { return feature2; } set { feature2 = value; } }
        public float LineA { get { return lineA; } set { lineA = value; } }
        public float LineB { get { return lineB; } set { lineB = value; } }
        public float CX { get { return cx; } set { cx = value; } }
        public float CY { get { return cy; } set { cy = value; } }
        public float Radius { get { return radius; } set { radius = value; } }
    }

    public class AnomalyReport
    {
        private string description;
        public string Description { get { return description; } set { description = value; } }
        private long timeStep;
        public long TimeStep { get { return timeStep; } set { timeStep = value; } }
    }
    #endregion
    public class DataBase : BaseModel
    {
        public static AutoResetEvent mre = new AutoResetEvent(false);
        #region UserInput members

        private string csvPath;
        private string[] csvLines;
        private int csvSize;
        private string xmlPath;
        private string fgPath;
        private bool regAlgo = false;
        private bool circleAlgo = false;

        #endregion

        #region Static resources

        private static string path = Directory.GetCurrentDirectory();
        private static string csvLearnPath = path + "\\..\\..\\Resources\\reg_flight.csv";
        private string newCsvLearnPath = csvLearnPath.Remove(csvLearnPath.Length - 4) + "_with_headers.csv";
        private string simplyAnomalyDetectionDLLPath = path + "\\..\\..\\Resources\\SimpleAnomalyDetectorDLL.dll";
        private string minCircleAnomalyDetectionDLLPath = path + "\\..\\..\\Resources\\MinCircleDetectorDLL.dll";

        #endregion

        #region General DataBase members

        private AnomalyDetection anomalyDetection;
        private List<CorrelatedFeatures> correlatedFeaturesList;
        private List<AnomalyReport> anomalyReportList;
        private TimeSeries timeSeries;
        private Socket fgSocket;
        private ConnectionHandler aConnection;
        private Thread handlingThread;
        private string csvHeaders;
        private string newAnomalyCsvPath;

        #endregion

        #region Constructor
        public DataBase()
        {
            this.timeSeries = new TimeSeries();
            anomalyDetection = new AnomalyDetection();
            correlatedFeaturesList = new List<CorrelatedFeatures>();
            anomalyReportList = new List<AnomalyReport>();
            aConnection = new ConnectionHandler();
            aConnection.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged(e.PropertyName);
        }

        #endregion

        #region Methods

        //method to create a new csv with headers, withput destroying the old csv
        private void CreateCsvWithHeaders(string oldCsvPath, string newCsvPath)
        {
            //if we havn't read XML yet
            if (csvHeaders == null)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(XmlPath);
                XmlNodeList name = xmlDocument.GetElementsByTagName("name");
                StringBuilder csvHeadersBuilder = new StringBuilder();
                string newLine = Environment.NewLine;

                //get headers from XML
                for (int i = 0; i < (name.Count) / 2; i++)
                {
                    //if there are headers next to eachother with the same name - tag them with numbers
                    if (name[i].InnerXml == name[i + 1].InnerXml)
                    {
                        int j = 1;
                        while (name[i].InnerXml == name[i + 1].InnerXml)
                        {
                            csvHeadersBuilder.Append(name[i].InnerXml + j.ToString() + ",");
                            j++;
                            i++;
                        }
                        csvHeadersBuilder.Append(name[i].InnerXml + j.ToString() + ",");
                        continue;
                    }
                    else
                        csvHeadersBuilder.Append(name[i].InnerXml + ",");
                }
                //remove last comma
                csvHeadersBuilder.Remove(csvHeadersBuilder.Length - 1, 1);
                csvHeadersBuilder.Append(newLine);
                CsvHeaders = csvHeadersBuilder.ToString();
            }

            string CsvWithHeaders = CsvHeaders + File.ReadAllText(oldCsvPath);
            //Create a new csv
            File.WriteAllText(newCsvPath, CsvWithHeaders);
        }
        public void OnClosing()
        {
            //don't forget to close resources
            if (fgSocket != null)
                fgSocket.Disconnect(true);
            if (File.Exists(newAnomalyCsvPath))
                File.Delete(newAnomalyCsvPath);
            if (File.Exists(newCsvLearnPath))
                File.Delete(newCsvLearnPath);
        }
        private void detectAnomalies()
        {
            anomalyDetection.detectAnomalies(ref correlatedFeaturesList, ref anomalyReportList);
        }
        private void startHandling(string[] csvLines, Socket socket)
        {
            handlingThread = new Thread(() => aConnection.handle(csvLines, socket));
            handlingThread.Start();
        }

        public void start()
        {
            CreateCsvWithHeaders(csvLearnPath, newCsvLearnPath);
            CreateCsvWithHeaders(CsvPath, newAnomalyCsvPath);
            detectAnomalies();
        }
        public void changeAlgo()
        {
            CorrelatedFeatures = new List<CorrelatedFeatures>();
            AnomalyReports = new List<AnomalyReport>();
            detectAnomalies();
        }

        public bool connect()
        {
            fgSocket = new ClientFG().connect();
            if (fgSocket != null)
            {
                startHandling(csvLines, fgSocket);
                return true;
            }
            else
                return false;
        }

        #endregion


        #region Properties

        public string NewAnomalyCsvPath
        {
            get
            {
                return newAnomalyCsvPath;
            }
        }
        public string NewCsvLearnPath
        {
            get
            {
                return newCsvLearnPath;
            }
        }
        public string CsvHeaders
        {
            get
            {
                return csvHeaders;
            }
            set
            {
                csvHeaders = value;
            }
        }
        public List<CorrelatedFeatures> CorrelatedFeatures
        {
            get
            {
                return this.correlatedFeaturesList;
            }
            set
            {
                this.correlatedFeaturesList = value;
                NotifyPropertyChanged(nameof(CorrelatedFeatures));
            }
        }
        public bool Running
        {
            set
            {
                //when simulation finishes, the thread stops. if user want to start again we need to startHandling again.
                if (handlingThread != null && !handlingThread.IsAlive)
                {
                    startHandling(csvLines, fgSocket);
                }
                if (aConnection.Running != value)
                {
                    //true == play -> signal the thread to stop sleeping 
                    if (value == true)
                    {
                        mre.Set();
                    }
                    aConnection.Running = value;
                }
            }
        }

        public List<AnomalyReport> AnomalyReports
        {
            get
            {
                return this.anomalyReportList;
            }
            set
            {
                this.anomalyReportList = value;
                NotifyPropertyChanged(nameof(AnomalyReports));
            }
        }
        public string FGPath
        {
            get
            {
                return this.fgPath;
            }
            set
            {
                this.fgPath = value;
                NotifyPropertyChanged(nameof(FGPath));
            }
        }
        public string CsvPath
        {
            get
            {
                return this.csvPath;
            }
            set
            {
                this.csvPath = value;
                newAnomalyCsvPath = csvPath.Remove(csvPath.Length - 4) + "_with_headers.csv";
                this.csvLines = File.ReadAllLines(csvPath);

                //Console.WriteLine("Im here");
                //Console.WriteLine(this.csvLines.Length);
                CsvSize = csvLines.Length - 1;
                this.timeSeries = new TimeSeries(csvPath);
                this.anomalyDetection.CsvLearnPath = newCsvLearnPath;
                this.anomalyDetection.CsvPath = newAnomalyCsvPath;
                NotifyPropertyChanged(nameof(CsvPath));
            }
        }
        public int CsvSize
        {
            get
            {
                return this.csvSize;
            }
            set
            {
                if (csvSize != value)
                {
                    csvSize = value;
                    NotifyPropertyChanged(nameof(CsvSize));
                }

            }
        }
        public string XmlPath
        {
            get
            {
                return this.xmlPath;
            }
            set
            {
                this.xmlPath = value;
                NotifyPropertyChanged(nameof(XmlPath));

            }
        }
        public bool RegAlgo
        {
            get
            {
                return this.regAlgo;
            }
            set
            {
                this.regAlgo = value;
                this.anomalyDetection.DllPath = simplyAnomalyDetectionDLLPath;
                NotifyPropertyChanged(nameof(RegAlgo));

            }
        }
        public bool CircleAlgo
        {
            get
            {
                return this.circleAlgo;
            }
            set
            {
                this.circleAlgo = value;
                this.anomalyDetection.DllPath = minCircleAnomalyDetectionDLLPath;
                NotifyPropertyChanged(nameof(CircleAlgo));
            }
        }
        public TimeSeries TimeSeries
        {
            get
            {
                return this.timeSeries;
            }
            set
            {
                this.timeSeries = value;
            }
        }
        public int TimeStep
        {
            get
            {
                return aConnection.TimeStep;
            }
            set
            {
                if (aConnection.TimeStep != value)
                {
                    aConnection.TimeStep = value;
                    NotifyPropertyChanged(nameof(TimeStep));
                }
            }

        }

        public double Speed
        {
            set
            {
                if (aConnection.Speed != value)
                {
                    aConnection.Speed = value;
                }
            }

        }
        #endregion

    }
}