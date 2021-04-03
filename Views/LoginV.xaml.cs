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
using System.Diagnostics;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for LoginV.xaml
    /// </summary>
    public partial class LoginV : Window
    {
        private string csvPath;
        private string xmlPath;
        private bool regAlgo;
        private bool circleAlgo;
        public string CsvPath
        {
            get
            {
                return this.csvPath;
            }
            set
            {
                this.csvPath = value;
            }
        }
        public string XmlPath
        {
            get
            {
                return this.XmlPath;
            }
            set
            {
                this.XmlPath = value;
            }
        }
        public bool RegAlgo
        {
            get
            {
                return this.regAlgo;
            }
            set
            {
                this.regAlgo = value;
            }
        }
        public bool CircleAlgo
        {
            get
            {
                return this.circleAlgo;
            }
            set
            {
                this.circleAlgo = value;
            }
        }




        public LoginV()
        {
            InitializeComponent();
        }
        
        private void b1Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                b1.Content = openFileDlg.FileName;
                this.csvPath = openFileDlg.FileName;

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
                 //  TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
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
                Process pros = Process.Start(b3.Content.ToString()); //open FG

            }
        }
        private void startClick(object sender, RoutedEventArgs e)
        {   
            //check which algotithm selected
            if((bool)regressionAlgo.IsChecked)
            {
                this.RegAlgo = true;
            } 
            else
            {
                this.CircleAlgo = true;
            }

            this.Close(); //close loginview and show mainview
        }
    }
}
