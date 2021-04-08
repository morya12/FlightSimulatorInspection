using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    //for mor- watch 4.2 11 minute
    class Point
    {
        float x { get; set; }
        float y { get; set; }
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Graph
    {

        int timeStep { get; set; }
        float featureAValue;
        public float FeatureAValue
        {
            get { return this.featureBValue; }
        }
        float featureBValue;
        public float FeatureBValue
        {
            get { return this.featureBValue; }
        }
        string featureA;
        public string FeatureA
        {
            get { return this.featureA; }
            set
            {
                if (this.featureA != value)
                {
                    this.featureA = value;
                    Console.WriteLine(FeatureA);
                    NotifyPropertyChanged("featureA");

                }
            }
        }


        string featureB;
        public string FeatureB
        {
            get { return this.featureB; }
            set
            {
                if (this.featureB != value)
                    this.featureB = value;
            }
        }
        List<Point> dataByFeature; // to do list of float
        List<Point> correlatedData;
        string section6Choise;
        List<float> section7FeatureACol;
        List<float> section7FeatureBCol;
        timeSeries ts;
        //public Graph(timeSeries ts)
        public Graph()
        {
            //this.ts = ts;
            this.timeStep = 0;
            this.section6Choise = null;
            this.dataByFeature = new List<Point>();
            this.correlatedData = new List<Point>();
        }

        public void calcSection6Graph(string feature)
        {
            if (feature != section6Choise)
            {
                this.dataByFeature = new List<Point>();
            }
            Point p = new Point(timeStep, ts.getData(section6Choise, timeStep));
            this.dataByFeature.Add(p);
        }

        public void calcSection7Graph(string feature1, string feature2)
        {
            List<float> featureA;
            List<float> featureB;

            if (section7FeatureACol == null)
            {
                featureA = ts.getFeatureDataCol(feature1);
                featureB = ts.getFeatureDataCol(feature2);
                this.section7FeatureACol = featureA;
                this.section7FeatureBCol = featureB;
            }
            else
            {
                featureA = this.section7FeatureACol;
                featureB = this.section7FeatureBCol;
            }

            this.correlatedData.Add(new Point(featureA[timeStep], featureB[timeStep]));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

