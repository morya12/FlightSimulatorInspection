using FlightSimulatorInspection.ViewModels;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for VideoV.xaml
    /// </summary>
    public partial class VideoV : UserControl
    {
        private VideoVM vm;
        public VideoV(VideoVM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = this.vm;
            WindowsFormsHost1.Child = this.vm.Panel;
            Trace.WriteLine("~~~~~~~~~~~~~~~~Video View CREATED~~~~~~~~~~~~~~~~~~~");

        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            vm.ResizeEmbeddedApp();
            return size;
        }
        public void OnClosing()
        {
            vm.OnClosing();
        }
    }
}
