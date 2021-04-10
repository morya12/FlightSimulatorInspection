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
        float featureBValue;
        string userChoise;
        string correlatedFeatureA;
        string correlatedFeatureB;
        List<float> featureACol;
        List<float> featureBCol;
        List<float> dataByFeature; // to do list of float
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
        public float FeatureBValue
        {
            get { return this.featureBValue; }
        }
        public string UserChoise
        {
            get { return this.userChoise; }
            set
            {
                if (this.userChoise != value)
                {
                    this.userChoise = value;
                    Console.WriteLine(UserChoise);
                    NotifyPropertyChanged("user choise");
                    this.featureACol = this.db.TimeSeries.getFeatureDataCol(this.userChoise);

                }
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
                    NotifyPropertyChanged("featureA");
                    this.featureACol = this.db.TimeSeries.getFeatureDataCol(this.correlatedFeatureA);
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
        public List<float> dataCol()
        {
            return this.db.TimeSeries.getFeatureDataCol(UserChoise);
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


    }
}

