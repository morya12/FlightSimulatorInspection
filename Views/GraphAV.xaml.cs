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

    public partial class GraphAV : UserControl, INotifyPropertyChanged
    {
        private double lastvalue;
        private double value;
        private List<float> data;
        private GraphVM vm;
        private string feature1;
        private List<float> featureACol;

        public List<float> FeatureACol
        {
            get
            {
                return this.featureACol;
            }
            set
            {
                this.featureACol = this.vm.getDataCol('A');
            }
        }

        public string Feature1
        {
            get
            {
                return this.feature1;
            }
            set
            {
                this.feature1 = value;
                OnPropertyChanged("Feature1");
            }
        }

    public GraphAV(GraphVM g)
    {
        InitializeComponent();
        this.vm = g;
        this.feature1 = "Feature A";
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
            while (true) 
            {
                int csvSize = vm.VM_CsvSize;
                int time = vm.VM_TimeStep;
                Thread.Sleep(500);

                if (System.Windows.Application.Current != null) 
                {
                    if (csvSize > 0 && time < csvSize)
                    {
                        //Console.WriteLine(csvSize);
                        //float x = this.featureACol[time];

                        this.data = this.vm.getDataCol('A');
                        this.Feature1 = this.vm.VM_FeatureA;
                        if (this.data != null &&  time < csvSize)
                        {
                             value = data[time];                        
                        }
                        else
                        {
                           value = 0;
                        }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        FeaturASeries[0].Values.Add(new ObservableValue(value));
                        FeaturASeries[0].Values.RemoveAt(0);
                        SetLecture();
                        });
                    }
                } 
                else
                {
                    Environment.Exit(0);
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
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}