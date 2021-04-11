using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorInspection.Models
{
    class ClientFG
    {
        private string csvFilePath;
        private string xmlFilePath;
        private int speed;
        public string XmlFilePath
        {
            get
            {
                return this.xmlFilePath;
            }
            set
            {
                this.xmlFilePath = value;
            }
        }
        public string CsvFilePath
        {
            get
            {
                return this.csvFilePath;
            }
            set
            {
                this.csvFilePath = value;
            }
        }
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
                this.speed = value;
            }
        }
        public int host = 6400;

        public ClientFG(string xmlFile, string csvFile)
        {
            this.xmlFilePath = xmlFile;
            this.csvFilePath = csvFile;
        }

        public ClientFG()
        {

        }

        public Socket connect()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, this.host);
            Console.WriteLine(remoteEP.ToString());
            s.Connect(remoteEP);
            return s;
            /*
            try
            {   
                string[] lines = File.ReadAllLines(CsvFilePath);
                byte[] bytes;
                int i = 0;

                foreach (string line in lines)
                {
                    bytes = Encoding.ASCII.GetBytes(lines[i]);
                    s.Send(bytes);
                    FlightStats.Instance.updateStatsTable(lines[i], i);
                    Thread.Sleep(100 / Speed);
                    i++;
                }
            }
            catch
            {
            }
            finally
            {
                s.Disconnect(true);
            }
            */
        }
    }
}
