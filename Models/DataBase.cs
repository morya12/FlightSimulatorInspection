using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulatorInspection.Models
{
    public class DataBase : BaseModel
    {
        #region UserInput members
        private string csvPath;
        private string xmlPath;
        private string fgPath;
        private bool regAlgo = false;
        private bool circleAlgo = false;
        private timeSeries timeSeries;
        private int timeStep;
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
                NotifyPropertyChanged(nameof(CircleAlgo));

            }
        }
        public timeSeries TimeSeries
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

    }
}
