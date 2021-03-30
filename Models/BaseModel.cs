
using System.ComponentModel;

namespace FlightSimulatorInspection.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        /*140
        260
        300 gift card
        200MB*/
    }
}
