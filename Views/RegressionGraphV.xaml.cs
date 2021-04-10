
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


namespace FlightSimulatorInspection.Views
{
    public partial class RegressionGraphV : UserControl
        {
        public double x = 3.5;
        public double y = 0;
        int counter = 0;

        public RegressionGraphV()
            {
                InitializeComponent();
            
        
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
                       new ScatterPoint( x, y, 5),

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
                       new ScatterPoint( x, y, 7),

                    },
                    PointGeometry = DefaultGeometries.Diamond,
                   // DataLabels = true,
                    Title = "annomly",
                    Fill = Brushes.Red,
                    //ScalesXAt = 100, only for acxes
                   // PointGeometry = Defa
                    //
                    
                    MinPointShapeDiameter = 7,
                    MaxPointShapeDiameter = 7
                },
                new LineSeries
                {
                    Title = "liner regrssion",
                    Values = new ChartValues<ObservablePoint> {
                        new ObservablePoint(x,y),
                        new ObservablePoint(62, 117),
                      
                    },
                    PointGeometry = null,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Transparent
                },
            };

                DataContext = this;
            }
            public SeriesCollection SeriesCollection { get; set; }

        

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var r = new Random();
                while (true)
                {
                    Thread.Sleep(500);
                    var series = SeriesCollection[1]; //red 

                    //foreach (var bubble in series.Values.Cast<ScatterPoint>())
                    //{
                    x += r.NextDouble();
                    y += r.NextDouble() + 0.5;

                    counter++;
                    series.Values.Add(new ScatterPoint(x, y));
                    if (counter > 30)
                    {
                        SeriesCollection[0].Values.Add(SeriesCollection[1].Values[0]);
                        SeriesCollection[1].Values.RemoveAt(0);
                    }

                    if (counter % 10 == 1)
                    {
                        SeriesCollection[2].Values.Add(new ScatterPoint(x+2, y+2,7));
                        //SeriesCollection[1].Values.RemoveAt(0);
                    }
                    //if (counter == 50) //clear
                    //{
                    //    SeriesCollection[0].Values.Clear();
                    //    SeriesCollection[1].Values.Clear();
                    //    SeriesCollection[2].Values.Clear();
                    //}
                }
            });
        }
    }
    }
