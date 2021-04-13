using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FlightSimulatorInspection.Models
{
    public class FlightStats : BaseModel
    {
        #region enum Stats
        public enum Stats
        {
            Aileron,
            Elevator,
            Rudder,
            Flaps,
            Slats,
            Speedbrake,
            Throttle,
            Throttle2,
            Engine_pump1,
            Engine_pump2,
            Electric_pump1,
            Electric_pump2,
            External_power,
            APU_generator,
            Latitude,
            Longitude,
            Altitude,
            Roll,
            Pitch,
            Heading,
            SideSlip,
            Airspeed,
            Glideslope,
            Vertical_speed_fps,
            Airspeed_indicator_indicated_speed_kt,
            Altimeter_indicated_altitude_ft,
            Altimeter_pressure_alt_ft,
            Attitude_indicator_indicated_pitch_deg,
            Attitude_indicator_indicated_roll_deg,
            Attitude_indicator_internal_pitch_deg,
            Attitude_indicator_internal_roll_deg,
            Encoder_indicated_altitude_ft,
            Encoder_pressure_alt_ft,
            Gps_indicated_altitude_ft,
            Gps_indicated_ground_speed_kt,
            Gps_indicated_vertical_speed,
            Indicated_heading_deg,
            Magnetic_compass_indicated_heading_deg,
            Slip_skid_ball_indicated_slip_skid,
            Turn_indicator_indicated_turn_rate,
            Vertical_speed_indicator_indicated_speed_fpm,
            Engine_rpm
        }
        #endregion


        #region Singleton design pattern
        private static readonly Lazy<FlightStats>
        lazy = new Lazy<FlightStats>(() => new FlightStats());
        public static FlightStats Instance { get { return lazy.Value; } }
        #endregion

        
        private FlightStats()
        {
            this.statsTable = new OrderedDictionary();
            #region init statsTable with Keys=Enum.Stats and Value=0.0f
            int len = Enum.GetNames(typeof(Stats)).Length;
            for (int i=0; i<len; i++)
            {
                statsTable.Add(Enum.GetName(typeof(Stats), i), 0.0);
            }
            
            #endregion
        }
        private OrderedDictionary statsTable;
        /// <summary>
        /// using Indexer to get and set values in our dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double this[string key]
        {
            get
            {
                if (statsTable.Contains(key))
                {
                    return (double)statsTable[key];
                }
                else
                    throw new KeyNotFoundException("Key: " + key + " doesn't exist in statsTable");
            }
            set
            {
                if (statsTable.Contains(key))
                {
                    if((double)statsTable[key] != value)
                    {
                        statsTable[key] = value;
                        NotifyPropertyChanged(key);
                    }
                }
            }
        }

        /// <summary>
        /// parse a new line with commas to float and update statsTable accordingly
        /// </summary>
        /// <param name="line"> specific line to parse </param>
        /// <param name="currentTimeStep"> timeS</param>
        public void updateStatsTable(string line)
        {
            string[] newVals = line.Split(',');
            int len = newVals.Length;
            if (len == statsTable.Count)
            {
                for (int i = 0; i < len; i++)
                {
                    string valName = Enum.GetName(typeof(Stats), i);
                    this[valName] = double.Parse(newVals[i]);
                }
            }
        }

    }
}
