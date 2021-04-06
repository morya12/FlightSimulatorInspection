using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    class GraphVM : BaseViewModel
    {
        Graph section6Graph;
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

        /*
        void LoadGraphView()
        {
            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 10;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canGraph.Width, ymax)));
            for (double x = xmin + step;
                x <= canGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);

            // Make some data sets.
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                int last_y = rand.Next((int)ymin, (int)ymax);

                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step)
                {
                    last_y = rand.Next(last_y - 10, last_y + 10);
                    if (last_y < ymin) last_y = (int)ymin;
                    if (last_y > ymax) last_y = (int)ymax;
                    points.Add(new Point(x, last_y));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                canGraph.Children.Add(polyline);
            }

        }
        */



    }
}
