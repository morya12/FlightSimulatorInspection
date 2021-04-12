using FlightSimulatorInspection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.ViewModels
{
    public class PlayerVM : BaseViewModel
    {
        private DataBase model;
        private ConnectionHandler connectionModel;
        public PlayerVM(DataBase db)
        {
            Trace.WriteLine("~~~~~~~~~~~~~~~~Player ViewModel CREATED~~~~~~~~~~~~~~~~~~~");

            this.model = db;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }
        //You may add Properties here

        public int VM_TimeStep
        {
            get
            {
                return model.TimeStep;
            }
            set
            {
                if(model.TimeStep != value)
                    model.TimeStep = value;
            }
        }
        public int VM_CsvSize
        {
            get
            {
                return model.CsvSize;
            }
        }
        public bool VM_Running
        {
            set
            {
                model.Running = value;
            }
        }
    }
}
