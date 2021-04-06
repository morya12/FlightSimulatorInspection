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
        private BaseModel model;
        public PlayerVM()
        {
            Trace.WriteLine("~~~~~~~~~~~~~~~~Player ViewModel CREATED~~~~~~~~~~~~~~~~~~~");

            this.model = FlightStats.Instance;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }
        //You may add Properties here
        public int VM_TimeStep
        {
            get
            {
                return (model as FlightStats).TimeStep;
            }
        }
    }
}
