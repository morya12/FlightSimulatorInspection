using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    public class ConnectionHandler
    {
        public static void readCSV()
        {
            var lines = File.ReadAllLines("C:\\Users\\97205\\source\\repos\\FlightSimulatorInspection\\Resources\\reg_flight.csv");
            new Thread(
                delegate () 
                {
                    for (int i = 1; i < lines.Length; i++)
                    {
                        FlightStats.Instance.updateStatsTable(lines[i], i);
                        Thread.Sleep(10);
                    }
                }).Start();

            

        }
    }
}
