using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #region UserInput members

        private string csvPath;
        private string xmlPath;
        private string fgPath;
        private bool regAlgo = false;
        private bool circleAlgo = false;
        private int timeStep;

        #endregion

        #region Static resources

        private string csvLearnPath = @"C:\Users\97205\source\repos\FlightSimulatorInspection\Resources\reg_flight.csv";
        private string simplyAnomalyDetectionDLLPath = @"C:\Users\97205\source\repos\FlightSimulatorInspection\Resources\SimpleAnomalyDetectorDLL.dll";
        private string minCircleAnomalyDetectionDLLPath = @"C:\Users\97205\source\repos\FlightSimulatorInspection\Resources\MinCircleDetectorDLL.dll";

        #endregion

        #region General DataBase members

        AnomalyDetection anomalyDetection = new AnomalyDetection();
        List<CorrelatedFeatures> correlatedFeaturesList = new List<CorrelatedFeatures>();
        List<AnomalyReport> anomalyReportList = new List<AnomalyReport>();
        private TimeSeries timeSeries;

        #endregion

        #region Properties
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
                this.timeSeries = new TimeSeries(csvPath);
                this.anomalyDetection.CsvLearnPath = csvLearnPath;
                this.anomalyDetection.CsvPath = csvPath;
                NotifyPropertyChanged(nameof(CsvPath));
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
                return this.timeStep;

            }
            set
            {
                this.timeStep = value;
            }
        }

        #endregion       

        #region Methods
        private void detectAnomalies()
        {
            anomalyDetection.detectAnomalies(ref correlatedFeaturesList, ref anomalyReportList);
        }
        public DataBase()
        {
            this.timeSeries = new TimeSeries();
        }
        

        public void start()
        {
            detectAnomalies();
        }
        #endregion


    }
}
