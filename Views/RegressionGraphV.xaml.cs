
using System;
using System.Collections.Generic;
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
    public partial class RegressionGraphV : UserControl
    {
        int counter = 0;
        private GraphVM vm;


        public RegressionGraphV(GraphVM g)
        {
            InitializeComponent();
            this.vm = g;

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries //blue
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                     //   new ScatterPoint(2.6, 5, 5),
                    },
                    Title = "old",
                    Fill = Brushes.Gray,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                },
                new ScatterSeries //red
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                      // new ScatterPoint( x, y, 5),
                    },
                    //PointGeometry = DefaultGeometries.Diamond,
                   // DataLabels = true,
                    Title = "last 30 sec",
                    Fill = Brushes.CornflowerBlue,
                    //ScalesXAt = 100, only for acxes
                   // PointGeometry = Defa
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                },
                new ScatterSeries //red
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                     //  new ScatterPoint( x, y, 7),

                    },
                    PointGeometry = DefaultGeometries.Diamond,
                   // DataLabels = true,
                    Title = "annomly",
                    Fill = Brushes.Red,
                    //ScalesXAt = 100, only for acxes
                    MinPointShapeDiameter = 7,
                    MaxPointShapeDiameter = 7
                },
                new LineSeries
                {
                    Title = "Liner regrssion",
                    Values = new ChartValues<ObservablePoint> {
                        new ObservablePoint(10,10),
                        //new ObservablePoint(62, 117),

                    },
                    PointGeometry = null,

                    Stroke = Brushes.Transparent,
                    Fill = Brushes.Transparent
                },

                new ScatterSeries //red
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                       new ScatterPoint( 50, 50, 100),// x,y,radius

                    },
                    Fill = Brushes.Transparent,
                    StrokeThickness = 1,
                    Stroke = Brushes.Transparent,
                    PointGeometry = DefaultGeometries.Circle,
                    //ScalesXAt = 100, only for acxes
                    Title = "Minimal Circle",
                    MinPointShapeDiameter = 100,
                    MaxPointShapeDiameter = 100
                },

            };

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }


        private void clear()
        {
                SeriesCollection[0].Values.Clear();
                SeriesCollection[1].Values.Clear();
                SeriesCollection[2].Values.Clear();
                SeriesCollection[3].Values.Clear();
                SeriesCollection[4].Values.Clear();
                counter = 0;
        }
        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            if (vm.VM_CircleAlgo)
            {
                ScatterSeries circle = (ScatterSeries)SeriesCollection[4];
                circle.Stroke = Brushes.Black;
                circle.Values.Add(new ScatterPoint(50, 50, 100));//x,y,radius

            }
            else
            {
                LineSeries l = (LineSeries)SeriesCollection[3];
                l.Stroke = Brushes.Black;
            }
            double i = 10;

                Task.Run(() =>
           {
                var r = new Random();
                while (true)
                {
                    Thread.Sleep(500);
                    var series = SeriesCollection[1]; //blue 

                    i += r.NextDouble();
                    i += r.NextDouble() + 0.5;//only for checking

                    counter++;
                    series.Values.Add(new ScatterPoint(i, i));
                    if (counter > 30)
                    {
                        SeriesCollection[0].Values.Add(SeriesCollection[1].Values[0]);
                        SeriesCollection[1].Values.RemoveAt(0);
                    }

                    if (counter == 50)
                    {
                       //  SeriesCollection[2].Values.Add(new ScatterPoint(i+2, i+2,7));
                       
                    }

                }
            });
        }
    }
    }
