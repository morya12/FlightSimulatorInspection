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
        private DispatcherTimer timer;

        private PlayerVM vm;
        public PlayerV()
        {
            InitializeComponent();
            vm = new PlayerVM();
            DataContext = vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~PlayerV View CREATED~~~~~~~~~~~~~~~~~~~");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = mediaElement.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void Slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            mediaElement.Source = new Uri(filename);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Play();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }
    }
}
