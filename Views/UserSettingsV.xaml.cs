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
            vm.connect();
        }
    }
}
