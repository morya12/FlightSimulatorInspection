using FlightSimulatorInspection.Models;
using FlightSimulatorInspection.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for PlayerV.xaml
    /// </summary>
    public partial class PlayerV : UserControl
    {
        private int baseSpeed = 100;
        private PlayerVM vm;
        public PlayerV(DataBase db)
        {
            InitializeComponent();
            vm = new PlayerVM(db);
            DataContext = vm;
            Trace.WriteLine("~~~~~~~~~~~~~~~~PlayerV View CREATED~~~~~~~~~~~~~~~~~~~");
            for(double i=0.5; i<=2.0; i += 0.25)
            {
                if(i!=1.0)
                    changeSpeed.Items.Add(i.ToString());
                else
                    changeSpeed.Items.Add("Normal");
            }
            vm.VM_Speed = baseSpeed;
            changeSpeed.Text = "Normal";
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

        private void changeSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = (sender as ComboBox).SelectedItem as string;
            double selectedSpeed;
            if (str == "Normal")
            {
                selectedSpeed = 1.0;
            }
            else
                selectedSpeed = double.Parse(str);
            vm.VM_Speed = baseSpeed * (1 / selectedSpeed);
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

    }
}
