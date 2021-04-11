using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FlightSimulatorInspection.Models
{
    public class TimeSeries
    {
        private int numOfRows;
	    private int numOfCols;
        List<String> parameters;    // the parameters part of the table
        List<List<float>> info;     // the info part of the table

        private void setParameters()
        {
            this.parameters = new List<string>()
            { "aileron",
            "elevator",
            "rudder",
            "flaps",
            "slats",
            "speedbrake",
            "throttle1",
            "throttle2",
            "engine_pump1",
            "engine_pump2",
            "electric_pump1",
            "electric_pump2",
            "external_power",
            "APU_generator",
            "latitude",
            "longitude",
            "altitude",
            "roll",
            "pitch",
            "heading",
            "sideSlip",
            "airspeed",
            "glideslope",
            "vertical_speed_fps",
            "airspeed_indicator_indicated_speed_kt",
            "altimeter_indicated_altitude_ft",
            "altimeter_pressure_alt_ft",
            "attitude_indicator_indicated_pitch_deg",
            "attitude_indicator_indicated_roll_deg",
            "attitude_indicator_internal_pitch_deg",
            "attitude_indicator_internal_roll_deg",
            "encoder_indicated_altitude_ft",
            "encoder_pressure_alt_ft",
            "gps_indicated_altitude_ft",
            "gps_indicated_ground_speed_kt",
            "gps_indicated_vertical_speed",
            "indicated_heading_deg",
            "magnetic_compass_indicated_heading_deg",
            "slip_skid_ball_indicated_slip_skid",
            "turn_indicator_indicated_turn_rate",
            "vertical_speed_indicator_indicated_speed_fpm",
            "engine_rpm"};
        }
        public TimeSeries(string csvFileName)
        {
            setParameters();
            if (!File.Exists(csvFileName))
            {
                Console.WriteLine("file not found"); // need to print on screen 
            }
            else
            {
                string[] lines = File.ReadAllLines(csvFileName).Skip(1).ToArray();
                this.numOfCols = parameters.Count();
                List<List<float>> csvListOfLists = new List<List<float>>();   // handle info
                string[] csvContentInString;
                int i = 0;
                foreach (string line in lines)
                {
                    csvContentInString = line.Split(',');
                    int j = 0;
                    foreach (var word in csvContentInString)
                    {
                        if (i == 0)
                        {
                            csvListOfLists.Add(new List<float>());
                        }
                        csvListOfLists[j].Add(float.Parse(word));
                    j++;
                    }
                i++;
                }
                this.info = csvListOfLists;
                this.numOfRows = csvListOfLists.Count();
            }

        }
        public TimeSeries()
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
                //Console.WriteLine(parameters[i]);
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
