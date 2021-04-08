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
        private DataBase db;
        float featureAValue;
        float featureBValue;
        string featureA;
        string featureB;
        List<float> featureACol;
        List<float> featureBCol;
        List<float> dataByFeature; // to do list of float
        timeSeries ts;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> parameters;


        public Graph()
        {
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
                    this.featureACol = this.ts.getFeatureDataCol(this.featureA);

                }
            }
        }
        public string FeatureB
        {
            get { return this.featureB; }
            set
            {
                if (this.featureB != value)
                {
                    this.featureB = value;
                    NotifyPropertyChanged("featureB"); 
                    this.featureBCol = this.ts.getFeatureDataCol(this.featureB);

                }
            }
        }
        public List<string> Parameters
        {
            get
            {
                if (this.parameters.Count == 0)
                {
                    this.parameters = ts.Parameters();
                }
                return this.parameters;
            }
            set
            {
                //
            }
           
        }
      

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        
    }
}

