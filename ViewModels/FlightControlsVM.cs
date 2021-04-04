using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorInspection.Views;
using FlightSimulatorInspection.Models;
using System.ComponentModel;

namespace FlightSimulatorInspection.ViewModels
{
    public class FlightControlsVM : BaseViewModel
    {
        //You may add Properties here
        public double VM_Rudder
        {
            get
            {
                return model[FlightStats.Stats.Rudder.ToString()];
            }
        }
        public double VM_Throttle
        {
            get
            {
                return model[FlightStats.Stats.Throttle.ToString()];
            }
        }
    }
}
