using FlightSimulatorInspection.ViewModels;
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
    /// Interaction logic for UserSettingsV.xaml
    /// </summary>
    public partial class UserSettingsV : UserControl
    {
        private UserSettingsVM vm;
        public UserSettingsV(UserSettingsVM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = this.vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.connect())
                (sender as Button).Visibility = Visibility.Hidden;
            else
                (sender as Button).Content = "Please try again in a moment";
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Brushes.Black;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FFE8E8E8");
            (sender as Button).Foreground = brush;
        }

        private void regressionAlgo_Checked(object sender, RoutedEventArgs e)
        {
            if (minCircleAlgo.IsChecked == true)
            {
                minCircleAlgo.IsChecked = false;
            }
            vm.changeAlgo("regressionAlgo");
        }

        private void minCircleAlgo_Checked(object sender, RoutedEventArgs e)
        {
            if (regressionAlgo.IsChecked == true)
            {
                regressionAlgo.IsChecked = false;
            }
            vm.changeAlgo("minCircleAlgo");
        }
    }
}
