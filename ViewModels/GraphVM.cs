using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorInspection.Models;
using System.ComponentModel;


namespace FlightSimulatorInspection.ViewModels
{

    public class GraphVM : BaseViewModel
    {
        Graph graphModel;
        Point xRange;
        Point yRange;
      

        public string VM_FeatureA
        {
            get { return graphModel.CorrelatedFeatureA; }
            set
            {
                if (graphModel.CorrelatedFeatureA != value)
                    graphModel.CorrelatedFeatureA = value;
                NotifyPropertyChanged("FeatureA");
            }
        }
        public string VM_FeatureB
        {
            get { return graphModel.CorrelatedFeatureB; }
        }

        public bool VM_RegAlgo
        {
            get
            {
                return graphModel.RegAlgo;
            }
        }
        public bool VM_CircleAlgo
        {
            get
            {
                return graphModel.CircleAlgo;
            }
        }

        public List<AnomalyReport> VM_RelevantReports
        {
            get
            {
                return graphModel.RelevantReports;
            }

        }
        public GraphVM(Graph model, DataBase db)
        {
            this.graphModel = model;
            this.graphModel.DB = db;
            graphModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
                Console.WriteLine("Gfgggg");
            };
        }

        public List<string> getParameters()
        {
            return this.graphModel.Parameters;
        }
        public List<float> getDataCol(char c)
        {
            return this.graphModel.dataCol(c);
        }
        
        public Point XRange
        {
            get
            {
                    xRange = this.graphModel.getRange('A');
                
                return xRange;
            }
        }
        public Point YRange
        {
            get
            {
                
                    yRange = this.graphModel.getRange('B');
                
                return yRange;
            }
        }
        public int VM_TimeStep
        {
            get
            {
                return graphModel.TimeStep;
            }
        }
        public int VM_CsvSize
        {
            get
            {
                return graphModel.CsvSize;
            }
        }
        public CorrelatedFeatures correlationData()
        {
            return this.graphModel.Correlated;
        }
        public bool isItAnomalyTime(int ts)
        {
            foreach (AnomalyReport a in graphModel.RelevantReports)
            {
                if (a.TimeStep == ts)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
