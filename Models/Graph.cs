using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{

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
    class Graph
    {
        
        int timeStep { get; set; }

        List<Point> dataByFeature;
        List<Point> correlatedData;
        string section6Choise;
        List<float> section7FeatureACol;
        List<float> section7FeatureBCol;
        timeSeries ts;
        public Graph(timeSeries ts)
        {
            this.ts = ts;
            this.timeStep = 0;
            this.section6Choise = null;
            this.dataByFeature = new List<Point>();
            this.correlatedData = new List<Point>();
        }

        void calcSection6Graph(string feature)
        {
            if (feature != section6Choise)
            {
                this.dataByFeature = new List<Point>();
            } 
                Point p  = new Point(timeStep, ts.getData(section6Choise, timeStep));
                this.dataByFeature.Add(p);
        }

        void calcSection7Graph(string feature1, string feature2)
        {
            List<float> featureA;
            List<float> featureB;

            if (section7FeatureACol == null)
            {
                featureA = ts.getFeatureDataCol(feature1);
                featureB = ts.getFeatureDataCol(feature2);
                this.section7FeatureACol = featureA;
                this.section7FeatureBCol = featureB;
            } else
            {
                featureA = this.section7FeatureACol;
                featureB = this.section7FeatureBCol;
            }

            this.correlatedData.Add(new Point(featureA[timeStep], featureB[timeStep]));
        }


    }
}

