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

        public void connect()
        {
            //model.connect();
        }
    }
}