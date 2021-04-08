using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FlightSimulatorInspection.Models
{
    public class timeSeries
    {
        private int numOfRows;
	    private int numOfCols;
        List<String> parameters;    // the parameters part of the table
        List<List<float>> info;     // the info part of the table

        public timeSeries()
        {
            this.numOfCols = 0;
            this.numOfRows = 0;
        }

        public timeSeries(string csvFileName)
        {
            string path = Directory.GetCurrentDirectory();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(path);
            sb.Append("/'").Append(csvFileName);

            if (!File.Exists(path))
            {
                Console.WriteLine("file not found"); // need to print on screen 
            }
            else
            {   
                List<string> csvParameters = new List<string>(); // handle parameters

                string[] lines = File.ReadAllLines(path);
                string[] paramsArr = lines[0].Split(',');
                foreach (var word in paramsArr)
                {
                    csvParameters.Add(word);
                }
                this.parameters = csvParameters;
                this.numOfCols = csvParameters.Count();


                List<List<float>> csvListOfLists = new List<List<float>>();   // handle info

                List<float> csvContentInFloat = new List<float>();
                string[] csvContentInString;
                bool ifFirstLine = true;
                foreach (string line in lines)
                {
                    csvContentInString = line.Split(',');
                    foreach (var word in csvContentInString)
                    {
                        csvContentInFloat.Add(float.Parse(word));
                    }
                    if (ifFirstLine)
                    {
                        ifFirstLine = false;
                        continue;   // skip adding to info
                    }
                    csvListOfLists.Add(csvContentInFloat);
                }
                this.info = csvListOfLists;
                this.numOfRows = csvListOfLists.Count();
            }

        }
        public int NumOfRows()
        {
            return this.numOfRows;
        }
        public void NumOfRows(int rows)
        {
            this.numOfRows = rows;
        }
        public int NumOfCols()
        {
            return this.numOfCols;
        }
        public void NumOfCols(int cols)
        {
            this.numOfRows = cols;
        }
        public List<string> Parameters()
        {
            return this.parameters;
        }
        public List<List<float>> Info()
        {
            return info;
        }
        public List<float> getFeatureDataCol(string feature)
        {
            int i = 0;
            foreach (string a in parameters)
            {
                if (a==feature) {
                    return info[i];
                }
                i++;
            }
            return null;
        }

        public float getData(string feature, int timeStep)
        {
            List<float> col = getFeatureDataCol(feature);
            return col[timeStep];
        }
    }
}
