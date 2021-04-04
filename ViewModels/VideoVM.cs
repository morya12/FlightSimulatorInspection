using FlightSimulatorInspection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.ViewModels
{
    class VideoVM : BaseViewModel
    {
        public string VM_SideSlip
        {
            get
            {
                return model[FlightStats.Stats.SideSlip.ToString()].ToString();
            }
        }
        public string VM_Airspeed
        {
            get
            {
                return model[FlightStats.Stats.Airspeed.ToString()].ToString();
            }
        }
        public string VM_Roll
        {
            get
            {
                return model[FlightStats.Stats.Roll.ToString()].ToString();
            }
        }
        public string VM_Pitch
        {
            get
            {
                return model[FlightStats.Stats.Pitch.ToString()].ToString();
            }
        }
        public string VM_Altimeter_indicated_altitude_ft
        {
            get
            {
                return model[FlightStats.Stats.Altimeter_indicated_altitude_ft.ToString()].ToString();
            }
        }
    }
}
