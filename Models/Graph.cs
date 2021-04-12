using System;
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
        float x;

        float y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

    }
    
    public class Graph
    {
        private DataBase db;
        string correlatedFeatureA;
        string correlatedFeatureB;
        List<float> featureACol;
        List<float> featureBCol;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> parameters;
        CorrelatedFeatures correlated;

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
        public string CorrelatedFeatureA
        {
            get { return this.correlatedFeatureA; }
            set
            {
                if (this.correlatedFeatureA != value)
                {
                    this.correlatedFeatureA = value;
                    //Console.WriteLine(correlatedFeatureA);
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

        public CorrelatedFeatures Correlated
        {
            get
            {
                return this.correlated;
            }
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
            Point p = new Point(dataCol[minIndex], dataCol[maxIndex]);
            //Console.Write(p.X);
            //Console.WriteLine(p.Y);
            return p;
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
            CorrelatedFeatures c = new CorrelatedFeatures();
            foreach (CorrelatedFeatures f in list)
            {
                if ((f.Feature1 == this.CorrelatedFeatureA) || (f.Feature2 == this.CorrelatedFeatureA))
                {
                    List<float> a = this.db.TimeSeries.getFeatureDataCol(f.Feature1);
                    List<float> b = this.db.TimeSeries.getFeatureDataCol(f.Feature2);
                    float res = pearson(a, b);
                    if (maxCorr < res)
                    {
                        maxCorr = res;
                        this.correlated = f;
                        c = f;
                    }
                }
                index++;
            }
            if (this.CorrelatedFeatureA == c.Feature1)
            {
                this.CorrelatedFeatureB = c.Feature2;
              
            } else if (this.CorrelatedFeatureA == c.Feature2)
            {
                this.CorrelatedFeatureB = c.Feature1;
            } else
            {
                Console.WriteLine("didnt find match");
                this.featureBCol = new List<float>();
                this.correlatedFeatureB = null;
                return;
            }
            this.featureBCol = this.db.TimeSeries.getFeatureDataCol(CorrelatedFeatureB);
        }

        //public bool isAnnomaly()
        //{
           // this.DB.Anno
        //}

        
    }
}

