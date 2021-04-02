using FlightSimulatorInspection.ViewModels;
using System.Windows.Controls;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for VideoV.xaml
    /// </summary>
    public partial class VideoV : UserControl
    {
        private VideoVM vm;
        public VideoV()
        {
            InitializeComponent();
            this.vm = new VideoVM();
            DataContext = vm;
        }
    }
}
