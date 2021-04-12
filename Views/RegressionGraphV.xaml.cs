
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
        Point lineData;
        int counter = 0;
        private GraphVM vm;
        private List<float> featureACol;
        private List<float> featureBCol;

        public Point LineData {
            get
            {
                if (this.lineData == null)
                {
                    this.lineData = new Point(this.vm.lineData().X, this.vm.lineData().Y);
                }
                return this.lineData;
            }
        }

        public List<float> FeatureACol{
            get
            {
                return this.featureACol;
            }
            set
            {
                this.featureACol = this.vm.getDataCol('A');
            }
        }
        public List<float> FeatureBCol
        {
            get
            {
                return this.featureBCol;
            }
            set
            {
                this.featureBCol = this.vm.getDataCol('B');
            }
        }

        public RegressionGraphV(GraphVM g)
        {
            InitializeComponent();
            this.vm = g;

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries //grey
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                     //   new ScatterPoint(2.6, 5, 5), //xP,yP,r
                    },
                    Title = "old",
                    Fill = Brushes.Gray,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                },
                new ScatterSeries //blue
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

                    Title = "Liner regrssion", // need to calc two range points and draw a line between
                    Values = new ChartValues<ObservablePoint> {
                    
                    //new ObservablePoint(start.X,start.Y),
                    //new ObservablePoint(end.X, end.Y),
                    },
                    PointGeometry = null,

                    Stroke = Brushes.Transparent,
                    Fill = Brushes.Transparent
                },

                new ScatterSeries // transparent
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
           
            FeatureACol = null;
            FeatureBCol = null;
            if (FeatureACol == null) {
                return;
            }
            if (vm.VM_CircleAlgo)
            {
                ScatterSeries circle = (ScatterSeries)SeriesCollection[4];
                circle.Stroke = Brushes.Black;
                circle.Values.Add(new ScatterPoint(50, 50, 100));//x,y,radius

            }
            else
            {
                FeatureACol = null;
                FeatureBCol = null;
                if (featureBCol == null)
                {
                    clear();
                    return;
                }
                float XValOfStart = this.vm.XRange.X;
                float XValOfEnd = this.vm.XRange.Y;
                Point lineData = new Point(this.vm.lineData().X, this.vm.lineData().Y);

                float YValOfStart = (float)((XValOfStart * lineData.X) + lineData.Y);
                float YValOfEnd = (float)((XValOfEnd * lineData.X) + lineData.Y);

                Point start = new Point(XValOfStart, YValOfStart);
                Point end = new Point(XValOfEnd, YValOfEnd);

                LineSeries l = (LineSeries)SeriesCollection[3];
                l.Values.Add(new ObservablePoint(start.X, start.Y));

                l.Values.Add(new ObservablePoint(end.X, end.Y));
               
                l.Stroke = Brushes.Black;
            }
            int i = 0;
                Task.Run(() =>
           {
               FeatureACol= null;
               FeatureBCol = null;

               while (true)
               {
                   Thread.Sleep(500);
                   if (System.Windows.Application.Current != null)
                   {
                       var series = SeriesCollection[1]; //blue 

                   float x = this.featureACol[i];
                       if (featureBCol == null)
                       {
                           continue;
                       }
                   float y = this.featureBCol[i];

                       counter++;
                       series.Values.Add(new ScatterPoint(x, y));
                       if (counter > 30)
                       {
                           SeriesCollection[0].Values.Add(SeriesCollection[1].Values[0]);
                           SeriesCollection[1].Values.RemoveAt(0);
                       }

                       if (counter == 50)
                       {
                           //  SeriesCollection[2].Values.Add(new ScatterPoint(i+2, i+2,7));

                       }
                       i++;

                   }
               }
            });
        }
    }
    }
