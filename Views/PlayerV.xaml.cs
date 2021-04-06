using FlightSimulatorInspection.ViewModels;
using System.Diagnostics;
using System.Windows.Controls;
namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for PlayerV.xaml
    /// </summary>
    public partial class PlayerV : UserControl
    {
        private PlayerVM vm;
        public PlayerV()
        {
            InitializeComponent();
            vm = new PlayerVM();
            DataContext = vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~PlayerV View CREATED~~~~~~~~~~~~~~~~~~~");

        }
    }
}
