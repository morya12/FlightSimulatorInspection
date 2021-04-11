﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    //for mor- watch 4.2 11 minute
    public class Point
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
        private DataBase db;
        float featureAValue;
        string correlatedFeatureA;
        string correlatedFeatureB;
        List<float> featureACol;
        List<float> featureBCol;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> parameters;


        public Graph()
        {
            //this.db = this.
        }
        public DataBase DB
        {
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
            }
        }
        public float FeatureAValue
        {
            get { return this.featureAValue; }
        } 
        public bool RegAlgo
        {
            get
            {
                return db.RegAlgo;
            }
        }
        public bool CircleAlgo
        {
            get
            {
                return db.CircleAlgo;
            }
        }
        public string CorrelatedFeatureA
        {
            get { return this.correlatedFeatureA; }
            set
            {
                if (this.correlatedFeatureA != value)
                {
                    this.correlatedFeatureA = value;
                    Console.WriteLine(correlatedFeatureA);
                    NotifyPropertyChanged("FeatureA");

                    this.featureACol = this.db.TimeSeries.getFeatureDataCol(this.correlatedFeatureA);
                    findMaxCorrelation();
                }
            }

        }
        public string CorrelatedFeatureB
        {
            get { return this.correlatedFeatureB; }
            set
            {
                if (this.correlatedFeatureB != value)
                {
                    this.correlatedFeatureB = value;
                    NotifyPropertyChanged("featureB"); 
                    this.featureBCol = this.db.TimeSeries.getFeatureDataCol(this.correlatedFeatureB);
                }
            }
        }
        public List<string> Parameters
        {
            get
            {
                return this.DB.TimeSeries.Parameters();
            }
            set
            {
                //
            }
           
        }
        public List<float> dataCol(char c)
        {
            if (c == 'A')
            {
                return this.db.TimeSeries.getFeatureDataCol(CorrelatedFeatureA);
            }
            return this.db.TimeSeries.getFeatureDataCol(CorrelatedFeatureB);
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public Point getRange(char c)
        {
            List<float> dataCol = this.featureACol;
            if (c == 'B')
            {
                dataCol = this.featureBCol;
            }
            float max = float.MinValue;
            float min = float.MaxValue;
            int index = 0;
            int maxIndex = -1;
            int minIndex = -1;
            foreach (float f in dataCol)
            {
                if (f > max)
                {
                    max = f;
                    maxIndex = index;
                }
                if (f < min)
                {
                    min = f;
                    minIndex = index;
                }

                index++;
            }
            return new Point(dataCol[minIndex], dataCol[maxIndex]);
        }
        float avg(List<float> x, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }
        float calculateAverage(List<float> x, float size)
        {
            float sum = 0;
            List <float> f = x;
            for (int i = 0; i < size; i++)
            {
                sum += f[i] / size;
            }
            return sum;
        }
        float var(List<float> x, int size)
        {
            float av = avg(x, size);
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }
        float cov(List<float> x, List<float> y, int size)
        {
            float aveX = calculateAverage(x, size);
            float aveY = calculateAverage(y, size);
            List<float> xy = new List<float>(size);
            Console.WriteLine(size);
            for (int i = 0; i < size; i++)
            {
                xy.Add(x[i] * y[i]);
            }
            float aveXY = calculateAverage(xy, size);

            float avgMul = (aveX * aveY);

            return aveXY - avgMul;
        }
        float pearson(List<float> x, List<float> y)
        {
            int size = x.Count();
            float f = (float)(cov(x, y, size) / (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size))));
            return f;
        }
        public void findMaxCorrelation()
        {
            List<CorrelatedFeatures> list = db.CorrelatedFeatures;

            float maxCorr = -1;
            int index = 0;
            int corrIndex = -1;
            string maxCorNameA = "--";
            string maxCorNameB = "--";
            List<CorrelatedFeatures> correlations = new List<CorrelatedFeatures>();
            int size = featureACol.Count();
            foreach (CorrelatedFeatures f in list)
            {
                Console.Write(f.Feature1);
                Console.Write(" , ");
                Console.WriteLine(f.Feature2);
                if ((f.Feature1 == this.CorrelatedFeatureA) || (f.Feature2 == this.CorrelatedFeatureA))
                {
                    correlations.Add(f);
                    List<float> a = this.db.TimeSeries.getFeatureDataCol(f.Feature1);
                    List<float> b = this.db.TimeSeries.getFeatureDataCol(f.Feature2);

                    float res = pearson(a, b);
                    if (maxCorr < res)
                    {
                        maxCorr = res;
                        corrIndex = index;
                        maxCorNameA = f.Feature1;
                        maxCorNameB = f.Feature2;
                    }
                }
                index++;
            }
            if (this.CorrelatedFeatureA == maxCorNameA)
            {
                this.CorrelatedFeatureB = maxCorNameB;
            } else if (this.CorrelatedFeatureA == maxCorNameB)
            {
                this.CorrelatedFeatureB = maxCorNameA;
            } else
            {
                Console.WriteLine("didnt fins match");
            }
            this.featureBCol = this.db.TimeSeries.getFeatureDataCol(CorrelatedFeatureB);
        }

    }
}

