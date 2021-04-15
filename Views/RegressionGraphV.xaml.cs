
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
using System.ComponentModel;

namespace FlightSimulatorInspection.Views
{
    public partial class RegressionGraphV : UserControl, INotifyPropertyChanged
    {
        int counter = 0;
        private GraphVM vm;
        private List<float> featureACol;
        private List<float> featureBCol;
        bool firstSelection = false;
        int threadC = 0;


        public event PropertyChangedEventHandler PropertyChanged;

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
            this.vm.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

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
                       //new ScatterPoint( 50, 50, 100),// x,y,radius

                    },
                    Fill = Brushes.Transparent,
                    StrokeThickness = 1,
                    Stroke = Brushes.Transparent,
                    PointGeometry = DefaultGeometries.Circle,
                    //ScalesXAt = 100, only for acxes
                    Title = "Minimal Circle",
                    //MinPointShapeDiameter = 1,
                   // MaxPointShapeDiameter = 100000
                },

                new ScatterSeries // transparent
                {
                    Values = new ChartValues<ScatterPoint> {},
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Transparent,
                },


            };

            DataContext = this;
        }

        private void NotifyPropertyChanged(string v)
        {
            if (v == "VM_FeatureA")
            {
                     Start();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }


        private void clear()
        {
                SeriesCollection[0].Values.Clear();
                SeriesCollection[1].Values.Clear();
                SeriesCollection[2].Values.Clear();
                SeriesCollection[3].Values.Clear();
                SeriesCollection[4].Values.Clear();
                SeriesCollection[5].Values.Clear();

            counter = 0;
        }
        private void Start()
        {
            FeatureACol = null;
            FeatureBCol = null;
            List<float> colA;
            List<float> colB;
            if (FeatureACol == null || FeatureBCol == null) {
                return;
            }
            if (this.vm.correlationData().Feature1 != this.vm.VM_FeatureA)
            {
                colA = featureBCol;
                colB = featureACol;
            } else
            {
                colA = featureACol;
                colB = featureBCol;
            }



                clear();
            if (vm.VM_CircleAlgo)
            {
                Point CircleData = new Point(this.vm.correlationData().CX, this.vm.correlationData().CY);
                float r = this.vm.correlationData().Radius;
                ScatterSeries circle = (ScatterSeries)SeriesCollection[4];
                circle.Stroke = Brushes.Black;
                circle.MaxPointShapeDiameter = 2.1*r;
                circle.MinPointShapeDiameter = 2.1*r;
                SeriesCollection[5].Values.Add(new ScatterPoint(CircleData.X - r, CircleData.X - r, 0));
                SeriesCollection[5].Values.Add(new ScatterPoint(CircleData.Y + r, CircleData.Y + r, 0));
             //   SeriesCollection[5].Values.Add(new ScatterPoint(0, 0, 0));
                circle.Values.Add(new ScatterPoint(CircleData.X,CircleData.Y,r));//x,y,radius
                LineSeries l = (LineSeries)SeriesCollection[3];
                                l.Values.Add(new ObservablePoint(CircleData.X - r, CircleData.Y));
                l.Fill = Brushes.Black;


            }
            else
            {
                float XValOfStart = this.vm.XRange.X;
                float XValOfEnd = this.vm.XRange.Y;
                Point lineData = new System.Windows.Point(this.vm.correlationData().LineA, this.vm.correlationData().LineB);

                float YValOfStart = (float)((XValOfStart * lineData.X) + lineData.Y);
                float YValOfEnd = (float)((XValOfEnd * lineData.X) + lineData.Y);

                Point start = new Point(XValOfStart, YValOfStart);
                Point end = new Point(XValOfEnd, YValOfEnd);

                LineSeries l = (LineSeries)SeriesCollection[3];
                l.Values.Add(new ObservablePoint(start.X, start.Y));
                l.Values.Add(new ObservablePoint(end.X, end.Y));
               
                l.Stroke = Brushes.Black;
            }
            int csvsize = vm.VM_CsvSize;
                Task.Run(() =>
           {

               threadC++;
               FeatureACol= null;
               FeatureBCol = null;

               while (true)
               {
                   int time = vm.VM_TimeStep;
                   Thread.Sleep(200);
                   if (System.Windows.Application.Current != null && time < csvsize)
                   {

                       var series = SeriesCollection[1]; //blue 

                       float x = this.featureACol[time];
                       if (featureBCol == null)
                       {
                           clear();
                           continue;
                       }
                       float y = this.featureBCol[time];
                       int anomlyCount = vm.VM_RelevantReports.Count();
                       if (anomlyCount > 0)
                       {
                           if (vm.VM_RelevantReports[0].TimeStep <= time && vm.VM_RelevantReports[anomlyCount - 1].TimeStep >= time)
                           {
                               for (int i = 0; i < anomlyCount; i++)
                               {
                                   if (vm.VM_RelevantReports[i].TimeStep == time)
                                   {
                                       SeriesCollection[2].Values.Add(new ScatterPoint(x, y));
                                       break;
                                   }
                               }
                           }

                       }
                       if (System.Windows.Application.Current != null)
                       {
                           series.Values.Add(new ScatterPoint(x, y));
                           if (counter > 30)
                           {
                               if (time > 2)
                               {
                                   SeriesCollection[0].Values.Add(SeriesCollection[1].Values[0]);
                                   SeriesCollection[1].Values.RemoveAt(0);
                               }
                           }
                       }
                       else
                       {
                           Environment.Exit(0);
                       }
                   }
                   counter++;

               }
               
           });
        }
    }
 }
