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

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for LoginV.xaml
    /// </summary>
    public partial class LoginV : Page
    {
        public LoginV()
        {
            InitializeComponent();
        }
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            /*// Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                b1.Content = openFileDlg.FileName;
                //  TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
                Console.WriteLine(openFileDlg.FileName);
                Console.WriteLine("here");
                //manager.infoFilePath = openFileDlg.FileName;
                Console.WriteLine("csv after");

            }*/
        }
        private void b2_Click(object sender, RoutedEventArgs e)
        {
           /* // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock

            if (result == true)
            {
                b2.Content = openFileDlg.FileName;
                Console.WriteLine("info before");

               //manager.setUpFilePath = openFileDlg.FileName;
                Console.WriteLine("info after");


                //  TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }*/
        }
        private void start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
