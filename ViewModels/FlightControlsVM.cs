using FlightSimulatorInspection.Views;
using FlightSimulatorInspection.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightSimulatorInspection.ViewModels
{
    public class FlightControlsVM : BaseViewModel
    {
        private BaseModel model;
        public FlightControlsVM()
        {
            Trace.WriteLine("~~~~~~~~~~~~~~~~FlightControls ViewModel CREATED~~~~~~~~~~~~~~~~~~~");

            this.model = FlightStats.Instance;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }
        //You may add Properties here
        public double VM_Rudder
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Rudder.ToString()];
            }
        }
        public double VM_Throttle
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Throttle.ToString()];
            }
        }
    }
}
