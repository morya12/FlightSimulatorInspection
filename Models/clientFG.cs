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
    class clientFG
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

        public clientFG(string xmlFile, string csvFile)
        {
            this.xmlFilePath = xmlFile;
            this.csvFilePath = csvFile;
        }

        public clientFG()
        {

        }

        public void connect()
        {

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, this.host);
            Console.WriteLine(remoteEP.ToString());
            s.Connect(remoteEP);
            //int i = 0;
            try
            {
                string[] lines = File.ReadAllLines(CsvFilePath);
                byte[] bytes;
                foreach (string line in lines)
                {
                    //Console.WriteLine(line);
                    //Console.WriteLine();
                    bytes = Encoding.ASCII.GetBytes(line);
                    s.Send(bytes);
                    Thread.Sleep(100 / Speed);
                }
            }
            catch
            {
            }
            finally
            {
                s.Disconnect(true);
            }
        }
    }
}
