using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    public class ConnectionHandler
    {
        public static void readCSV(string csvPath)
        {
            ClientFG c = new ClientFG();
            c.CsvFilePath=csvPath;
            Socket s = c.connect();

            string[] lines = File.ReadAllLines(csvPath);
            byte[] bytes;
            int i = 0;
            new Thread(delegate ()
            {
                foreach (string line in lines)
                {
                    bytes = Encoding.ASCII.GetBytes(lines[i]);
                    s.Send(bytes);
                    FlightStats.Instance.updateStatsTable(lines[i], i);
                    Thread.Sleep(100 / 1);
                    i++;
                }

                s.Disconnect(true);

            }).Start();
        }
    }
}
