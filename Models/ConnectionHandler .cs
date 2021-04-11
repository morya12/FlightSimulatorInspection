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
        public bool iterate;
        public int speed;
        public int Speed
        {
            get
            {
                if (speed == 0)
                {
                    return 1;
                }
                return speed;
            }
            set
            {
                if (value < 0)   //invalid speed. do not update
                {
                    return;
                }
                speed = value;
            }
        }
        public ConnectionHandler()
        {
            iterate = true;
        }
        public bool Iterate
        {
            get
            {
                return iterate;
            }
            set
            {
                iterate = value;
            }
        }
        public void readCSV(string csvPath)
        {
            ClientFG c = new ClientFG();
            c.CsvFilePath=csvPath;
            Socket s = c.connect();

            string[] lines = File.ReadAllLines(csvPath);
            byte[] bytes;
            new Thread(delegate ()
            {
                for (int i = 1; i<lines.Length; i++)
                {
                    if (Iterate)
                    {
                        bytes = Encoding.ASCII.GetBytes(lines[i]);
                        s.Send(bytes);
                        FlightStats.Instance.updateStatsTable(lines[i], i);
                        Thread.Sleep(100 / 1);

                    }
                }

                s.Disconnect(true);

            }).Start();
        }
    }
}
