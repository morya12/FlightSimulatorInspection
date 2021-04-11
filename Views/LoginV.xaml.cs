using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using FlightSimulatorInspection.Models;
using FlightSimulatorInspection.ViewModels;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for LoginV.xaml
    /// </summary>
    public partial class LoginV : UserControl
    {
        private LoginVM vm;
        public LoginV(LoginVM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = this.vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~LOGIN View CREATED~~~~~~~~~~~~~~~~~~~");
        }
        
        private void b1Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
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
            Nullable<bool> result = openFileDlg.ShowDialog();

            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                b2.Content = openFileDlg.FileName;
            }
        }

        private void b3Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
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
            ConnectionHandler.readCSV(vm.VM_CsvPath);
            //ClientFG fg = new ClientFG();
            //fg.CsvFilePath = vm.VM_CsvPath;
            //fg.connect();
            (this.Parent as Border).Visibility = Visibility.Collapsed;
            vm.start();


        }
    }
}
