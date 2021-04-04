using FlightSimulatorInspection.Views;
using FlightSimulatorInspection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorInspection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginV  loginWindow = new LoginV(); // open the login window before the main window
            loginWindow.ShowDialog();
            // checks if user insert csv file
            if(String.IsNullOrEmpty(loginWindow.CsvPath)){ 
                  this.Close();
            }
            else {
                ConnectionHandler.readCSV(loginWindow.CsvPath);
            }
        }

    }
}
