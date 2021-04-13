using FlightSimulatorInspection.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightSimulatorInspection.ViewModels
{
    public class PlayerVM : BaseViewModel
    {
        private DataBase model;
        public PlayerVM(DataBase db)
        {
            this.model = db;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }
        public int VM_TimeStep
        {
            get
            {
                return model.TimeStep;
            }
            set
            {
                if (model.TimeStep != value)
                    model.TimeStep = value;
            }
        }
        //Slider needs to know it's max --> each line in csv is a time tick
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
        public double VM_Speed
        {
            set
            {
                model.Speed = value;
            }
        }
    }
}
