using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using FlightSimulatorInspection.Models;
using FlightSimulatorInspection.ViewModels;
using System.Windows.Threading;
using System.Threading;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for LoginV.xaml
    /// </summary>
    public partial class LoginV : UserControl
    {
        private LoginVM vm;
        DispatcherTimer timer = new DispatcherTimer();

        public LoginV(LoginVM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = this.vm;
            string src = Environment.CurrentDirectory + "\\..\\..\\Images\\loadingGif.gif";
            media.Source = new Uri(src);
        }
        
        private void b1Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            if (result == true)
            {
                b1.Content = openFileDlg.FileName;
                vm.VM_CsvPath = openFileDlg.FileName;
            }
        }
        private void b2Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            openFileDlg.Filter = "XML Files (*.xml)|*.xml"; 
            Nullable<bool> result = openFileDlg.ShowDialog();

            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                b2.Content = openFileDlg.FileName;
                vm.VM_XmlPath = openFileDlg.FileName;
            }
        }

        private void b3Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            //openFileDlg.Filter = " Exe Files(.exe)| *.exe | All Files(*.*) | *.* ";
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                b3.Content = openFileDlg.FileName;
                vm.VM_FGPath = openFileDlg.FileName;
            }
        }
        private void startClick(object sender, RoutedEventArgs e)
        {
            //check which algotithm selected
            if ((bool)regressionAlgo.IsChecked)
                vm.VM_RegAlgo = true;
            else
                vm.VM_CircleAlgo = true;
            mainGrid.Visibility = Visibility.Collapsed;
            //open loading Screen
            loading();
            
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            media.Position = new TimeSpan(0, 0, 1);
            media.Play();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            media.Visibility = Visibility.Collapsed;
            (this.Parent as Border).Visibility = Visibility.Collapsed;
            vm.start();
        }

        private void loading()
        {
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }
    }
}
