using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorInspection;

namespace FlightSimulatorInspection.Models
{
    public class ConnectionHandler
    {
        public static void readCSV(string csvPath)
        {
            clientFG c = new clientFG();
            c.CsvFilePath=csvPath;
         
            
          //  var lines = File.ReadAllLines("C:\\Users\\97254\\source\\repos\\morya12\\FlightSimulatorInspection\\Resources\\reg_flight.csv");
           // var lines = File.ReadAllLines("C:\\Users\\97205\\source\\repos\\FlightSimulatorInspection\\Resources\\reg_flight.csv");
         var lines = File.ReadAllLines(csvPath);
            
            new Thread(
            delegate () 
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    FlightStats.Instance.updateStatsTable(lines[i], i);
                    Thread.Sleep(10);
                }
            }).Start();

            new Thread(
            delegate ()
            {
                c.connect();
            }).Start();
            //c.connect();

            // todo assign each thread to a different var and join() them (wait for finish)
        }
    }
}
