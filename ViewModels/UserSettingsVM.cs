using FlightSimulatorInspection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.ViewModels
{
    public class UserSettingsVM : BaseViewModel
    {
        private DataBase model;
        public UserSettingsVM(DataBase model)
        {
            this.model = model;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }

        public bool connect()
        {
            return model.connect();
        }
        public void changeAlgo(string algo)
        {
            model.Running = false;
            model.TimeStep = 1;
            if (algo == "regressionAlgo")
            {
                model.CircleAlgo = false;
                model.RegAlgo = true;
            }
            else if (algo == "minCircleAlgo")
            {
                model.CircleAlgo = true;
                model.RegAlgo = false;
            }
            model.changeAlgo();
        }
    }
}