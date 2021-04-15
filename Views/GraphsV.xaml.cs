using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using FlightSimulatorInspection.ViewModels;
using FlightSimulatorInspection.Models;



namespace FlightSimulatorInspection.Views
{
    /// <summary>
    /// Interaction logic for GraphsV.xaml
    /// </summary>
    public partial class GraphsV : UserControl
    {
        string feature1;
        string feature2;
        GraphVM graphVM;
        public GraphsV(DataBase db)
        {
            InitializeComponent();
            graphVM = new GraphVM(new Graph(), db);
            GraphABorder.Child = new GraphAV(graphVM);
            GraphBBorder.Child = new GraphBV(graphVM); //need to add graphVM to constrctor 
            
            RegressionGraphBorder.Child = new RegressionGraphV(graphVM);
            List<string> features = this.graphVM.getParameters();
            // here need to get list of graph
           foreach (string a in features)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = a;
                featureListBox.Items.Add(newItem);

            }           
        }

        private void featureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (featureListBox.SelectedItem != null)
            {
                
                this.graphVM.VM_FeatureA = (featureListBox.SelectedItem as ListBoxItem).Content.ToString();
                this.feature2 = graphVM.VM_FeatureB;

            }
        }

    }
}
