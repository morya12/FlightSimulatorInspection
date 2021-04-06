using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    public class ConnectionHandler
    {
        public static void readCSV(string csvPath)
        {
          //  var lines = File.ReadAllLines("C:\\Users\\97254\\source\\repos\\morya12\\FlightSimulatorInspection\\Resources\\reg_flight.csv");
            var lines = File.ReadAllLines("C:\\Users\\97205\\source\\repos\\FlightSimulatorInspection\\Resources\\reg_flight.csv");
            //var lines = File.ReadAllLines(csvPath);
            //ProcessStartInfo psi = new ProcessStartInfo("notepad.exe");

            //Process processEXE = Process.Start(psi);

            new Thread(
                delegate () 
                {
                    //need to add stop signal
                    for (int i = 1; i < lines.Length; i++)
                    {
                        FlightStats.Instance.updateStatsTable(lines[i], i);
                        Thread.Sleep(10);
                    }
                }).Start();

            

        }
    }
}
