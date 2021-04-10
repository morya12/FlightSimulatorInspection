﻿using System;
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
            get { return graphModel.UserChoise; }
            set
            {
                if (graphModel.UserChoise != value)
                    graphModel.UserChoise = value;
            }
        }
        public float VM_FeatureAValue
        {
            get { return graphModel.FeatureAValue; }
        }
        public string VM_FeatureB
        {
            get { return graphModel.CorrelatedFeatureB; }
            set
            {
                if (graphModel.CorrelatedFeatureB != value)
                    graphModel.CorrelatedFeatureB = value;
            }
        }
        public float VM_FeatureBValue
        {
            get { return graphModel.FeatureBValue; }
        }
        public GraphVM(Graph model, DataBase db)
        {
            this.graphModel = model;
            this.graphModel.DB = db;
            graphModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public List<string> getParameters()
        {
            return this.graphModel.Parameters;
        }
        public List<float> getDataCol()
        {
            return this.graphModel.dataCol();
        }

    }
}
