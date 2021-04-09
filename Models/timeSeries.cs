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

        private void setParameters()
        {
            this.parameters = new List<string>()
            { "Aileron",
            "Elevator",
            "Rudder",
            "Flaps",
            "Slats",
            "Speedbrake",
            "Throttle",
            "Throttle2",
            "Engine_pump1",
            "Engine_pump2",
            "Electric_pump1",
            "Electric_pump2",
            "External_power",
            "APU_generator",
            "Latitude",
            "Longitude",
            "Altitude",
            "Roll",
            "Pitch",
            "Heading",
            "SideSlip",
            "Airspeed",
            "Glideslope",
            "Vertical_speed_fps",
            "Airspeed_indicator_indicated_speed_kt",
            "Altimeter_indicated_altitude_ft",
            "Altimeter_pressure_alt_ft",
            "Attitude_indicator_indicated_pitch_deg",
            "Attitude_indicator_indicated_roll_deg",
            "Attitude_indicator_internal_pitch_deg",
            "Attitude_indicator_internal_roll_deg",
            "Encoder_indicated_altitude_ft",
            "Encoder_pressure_alt_ft",
            "Gps_indicated_altitude_ft",
            "Gps_indicated_ground_speed_kt",
            "Gps_indicated_vertical_speed",
            "Indicated_heading_deg",
            "Magnetic_compass_indicated_heading_deg",
            "Slip_skid_ball_indicated_slip_skid",
            "Turn_indicator_indicated_turn_rate",
            "Vertical_speed_indicator_indicated_speed_fpm",
            "Engine_rpm"};
        }
        public timeSeries(string csvFileName)
        {
            setParameters();
            if (!File.Exists(csvFileName))
            {
                Console.WriteLine("file not found"); // need to print on screen 
            }
            else
            {   
                string[] lines = File.ReadAllLines(csvFileName);
                this.numOfCols = parameters.Count();
                List<List<float>> csvListOfLists = new List<List<float>>();   // handle info
                string[] csvContentInString;
                bool firstTime = true;
                foreach (string line in lines)
                {
                    csvContentInString = line.Split(',');
                    int i = 0;
                    foreach (var word in csvContentInString)
                    {
                        if (firstTime)
                        {
                            csvListOfLists.Add(new List<float>());
                            firstTime = false;
                        }
                        csvListOfLists[i].Add(float.Parse(word));
                    }
                }
                this.info = csvListOfLists;
                this.numOfRows = csvListOfLists.Count();
            }

        }
        public timeSeries()
        {
            setParameters();

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
            int len = this.parameters.Count();
            for (int i = 0; i<len; i++)
            {
                if (this.parameters[i] == feature) {
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
