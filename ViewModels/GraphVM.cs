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
        Point xRange;
        Point yRange;
      

        public string VM_FeatureA
        {
            get { return graphModel.CorrelatedFeatureA; }
            set
            {
                if (graphModel.CorrelatedFeatureA != value)
                    graphModel.CorrelatedFeatureA = value;
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
        public List<float> getDataCol(char c)
        {
            return this.graphModel.dataCol(c);
        }
        
        public Point XRange
        {
            get
            {
                if (this.xRange == null) {
                    xRange = this.graphModel.getRange('A');
                }
                return xRange;
            }
        }
        public Point YRange
        {
            get
            {
                if (this.yRange == null)
                {
                    yRange = this.graphModel.getRange('B');
                }
                return yRange;
            }
        }

        public Point lineData()
        {
            return this.graphModel.LineData;
        }
    }
}
