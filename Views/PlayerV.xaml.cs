using FlightSimulatorInspection.Models;
using FlightSimulatorInspection.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for PlayerV.xaml
    /// </summary>
    public partial class PlayerV : UserControl
    {
        private DispatcherTimer timer;

        private PlayerVM vm;
        public PlayerV(DataBase db)
        {
            InitializeComponent();
            vm = new PlayerVM(db);
            DataContext = vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~PlayerV View CREATED~~~~~~~~~~~~~~~~~~~");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
         //   slider_seek.Value = mediaElement.Position.TotalSeconds;
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            vm.VM_Running = true;
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            vm.VM_Running = false;
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            vm.VM_Running = false;
            vm.VM_TimeStep = 1;
        }

        private void Slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //mediaElement.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

     //   private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
     //   {
     //       TimeSpan ts = //mediaElement.NaturalDuration.TimeSpan;
     //       slider_seek.Maximum = ts.TotalSeconds;
     //       timer.Start();
     //   }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            //mediaElement.Source = new Uri(filename);
            //mediaElement.LoadedBehavior = MediaState.Manual;
            //mediaElement.UnloadedBehavior = MediaState.Manual;
            //mediaElement.Play();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //mediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }
    }
}
