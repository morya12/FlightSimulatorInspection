using FlightSimulatorInspection.ViewModels;
using System.Diagnostics;
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
            vm = new FlightControlsVM();
            DataContext = vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~FlightControls View CREATED~~~~~~~~~~~~~~~~~~~");
        }

    }
}
