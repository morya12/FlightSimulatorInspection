using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using FlightSimulatorInspection.ViewModels;

namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class GraphBV : UserControl, INotifyPropertyChanged
    {
        private double lastvalue;
        private double value;
        private List<float> data;
        private GraphVM vm;

        public GraphBV(GraphVM g)
        {
            InitializeComponent();
            this.vm = g;

            FeaturASeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0)
                    }

                }
            };

            Task.Run(() =>
            {
                int i = 0;
                while (true)  // currently not in sync with simulator
                {
                    this.data = this.vm.getDataCol('B');
                    Thread.Sleep(500);
                    if (this.data != null && this.data.Any())
                    {
                        value = data[i];
                        i++;
                    }
                    else
                    {
                        value = 0;
                    }
                    //value = (r.NextDouble() > 0.3 ? 1 : -1) * r.Next(0, 5); //need to bind to feature 
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        FeaturASeries[0].Values.Add(new ObservableValue(value));
                        FeaturASeries[0].Values.RemoveAt(0);
                        SetLecture();

                    });

                }
            });


            DataContext = this;
        }

        public SeriesCollection FeaturASeries { get; set; }

        public double LastValue
        {
            get { return lastvalue; }
            set
            {
                lastvalue = value;
                OnPropertyChanged("LastValue");
            }
        }

        private void SetLecture()
        {
            var target = ((ChartValues<ObservableValue>)FeaturASeries[0].Values).Last().Value;
            // var step = (target - _lastLecture) / 4; // makes it look smooth



            Task.Run(() =>
            {
                //for (var i = 0; i < 4; i++)
                //{
                //    Thread.Sleep(100);
                //    LastLecture += step;
                //}
                LastValue = target;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}