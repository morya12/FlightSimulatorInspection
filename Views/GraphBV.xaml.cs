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
        private string feature2;

        public string Feature2
        {
            get
            {
                return this.feature2;
            }
            set
            {
                this.feature2 = value;
                OnPropertyChanged("Feature2");
            }
        }


        public GraphBV(GraphVM g)
        {
            InitializeComponent();
            this.vm = g;
            this.feature2 = "Feature B";
            this.DataContext = this;

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
                int csvSize = vm.VM_CsvSize;
                while (true)  // currently not in sync with simulator
                {
                    int time = vm.VM_TimeStep;
                    this.data = this.vm.getDataCol('B');
                    this.Feature2 = this.vm.VM_FeatureB;
                    Thread.Sleep(500);
                    if (this.data != null)
                    {
                        if (this.data != null && time < csvSize)
                        {
                            value = data[time];
                        }
                        else
                        {
                            value = 0;
                        }
                        if (System.Windows.Application.Current != null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                FeaturASeries[0].Values.Add(new ObservableValue(value));
                                FeaturASeries[0].Values.RemoveAt(0);
                                SetLecture();
                            });
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                }
            });
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
            Task.Run(() =>
            {
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