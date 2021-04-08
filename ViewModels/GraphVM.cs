using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorInspection.Models;
using System.ComponentModel;
using FlightSimulatorInspection.Models;


namespace FlightSimulatorInspection.ViewModels
{


    /*
    * section 6 -  the client need to see alist of patameters (from a list, ithink list box
    *      will match but its up to you.) 
    *so : 1. i wrote you a function that returns you the list<string> of parameters
    *      2. i wrote a func that recieve a parameter name and returns a list<point> axis x is time and 
    *      axis y is tha value in this curr timeStep (if i need to return anything else just say. 
    *      i wasnt really sure what i need to return
    *      
    * section 7 - need to view a graph of the most correlated feature (which shai is still working on)
    *      so i wrote a func that recieve 2 features(correlted) and return list of points if them
    *      
    *      both 6 and 7 - as tomeStep keeps going the list of points is updated. so we'll need to take the 
    *         latest point in the list (in 6 if the seleced feature is changed so the graph need to be deleted?)
    */


    public class GraphVM : BaseViewModel
    {
        Graph graphModel;
        public string VM_FeatureA
        {
            get { return graphModel.FeatureA; }
            set
            {
                if (graphModel.FeatureA != value)
                    graphModel.FeatureA = value;
            }
        }
        public float VM_FeatureAValue
        {
            get { return graphModel.FeatureAValue; }
        }
        public GraphVM(Graph g)
        {
            this.graphModel = g;
            graphModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        /*
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadListBoxData();
            LoadGraphView();
        }
        */


        void LoadListBoxData()
        {

        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
