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
      

        public string VM_FeatureA
        {
            get { return graphModel.FeatureA; }
            set
            {
                if (graphModel.FeatureA != value)
                    graphModel.FeatureA = value;
            }
        }
        public float VM_FeatureAValue
        {
            get { return graphModel.FeatureAValue; }
        }

        public string VM_FeatureB
        {
            get { return graphModel.FeatureB; }
            set
            {
                if (graphModel.FeatureB != value)
                    graphModel.FeatureB = value;
            }
        }
        public float VM_FeatureBValue
        {
            get { return graphModel.FeatureBValue; }
        }
        public GraphVM(Graph model)
        {
            this.graphModel = model;
            graphModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

    }
}
