using FlightSimulatorInspection.Views;
using FlightSimulatorInspection.Models;
using System.Windows;
using FlightSimulatorInspection.ViewModels;

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
            DataBase db = new DataBase();
            UserSettingsVBorder.Child = new UserSettingsV(new UserSettingsVM(db));
            LoginBorder.Child = new LoginV(new LoginVM(db));
            VideoBorder.Child = new VideoV(new VideoVM(db));
            PlayerBorder.Child = new PlayerV(db);
            GraphsVBorder.Child = new GraphsV(db);
            FlightControlsVBorder.Child = new FlightControlsV();

        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            //close FG.exe
            if(VideoBorder.Child != null)
                (VideoBorder.Child as VideoV).OnClosing();
        }

    }
}
