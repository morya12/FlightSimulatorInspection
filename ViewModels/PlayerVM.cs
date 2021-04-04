using FlightSimulatorInspection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.ViewModels
{
    public class PlayerVM : BaseViewModel
    {
        //You may add Properties here
        public string VM_Throttle
        {
            get
            {
                return model[FlightStats.Stats.Throttle.ToString()].ToString();
            }
        }
        public int VM_TimeStep
        {
            get
            {
                return model.TimeStep;
            }
        }
    }
}
