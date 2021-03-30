using FlightSimulatorInspection.Models;
using System;
using System.ComponentModel;

namespace FlightSimulatorInspection.ViewModels
{
    /// <summary>
    /// Base View Model for each vm to gain funcionality
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected BaseModel model;
        
        
    }

}
