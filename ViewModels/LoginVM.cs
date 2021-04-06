using FlightSimulatorInspection.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightSimulatorInspection.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        private string csvPath;
        private string xmlPath;
        private string fgPath;
        private bool regAlgo = false;
        private bool circleAlgo = false;

        private DataBase model;
        public LoginVM(DataBase model)
        {
            Trace.WriteLine("~~~~~~~~~~~~~~~~LOGIN ViewModel CREATED~~~~~~~~~~~~~~~~~~~");
            this.model = model;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }

        public string VM_FGPath
        {
            set
            {
                fgPath = value;
                model.FGPath = fgPath;
            }
        }

    }
}
