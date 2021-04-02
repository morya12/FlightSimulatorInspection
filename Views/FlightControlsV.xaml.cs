using FlightSimulatorInspection.ViewModels;
using System.Windows.Controls;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for FlightControlsV.xaml
    /// </summary>
    public partial class FlightControlsV : UserControl
    {
        private FlightControlsVM vm;
        public FlightControlsV()
        {
            InitializeComponent();
            this.vm = new FlightControlsVM();
            DataContext = vm;
        }

    }
}
