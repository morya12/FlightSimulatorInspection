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
        private string xmlUpFilePath;
        private int speed;
        public string SetUpFilePath
        {
            get
            {
                return this.xmlUpFilePath;
            }
            set
            {
                this.xmlUpFilePath = value;
            }
        }
        public string InfoFilePath
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

        public clientFG(string setUpFile, string infoFile)
        {
            this.xmlUpFilePath = setUpFile;
            this.csvFilePath = infoFile;
        }

        public clientFG()
        {

        }

        public void connect()
        {

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, this.host);
            Console.WriteLine(remoteEP.ToString());
            s.Connect(remoteEP);
            Console.WriteLine("connected"); // to remove
            //int i = 0;
            try
            {
                string[] lines = File.ReadAllLines(this.InfoFilePath);
                Console.Write(this.InfoFilePath);
                //string local_file_temp_xxx_todo_debug = "C:\\Users\\Mor\\Desktop\\לימודים\\תכנות מתקדם\\flightsimulator\\reg_flight.csv";
                //string[] lines = File.ReadAllLines(local_file_temp_xxx_todo_debug);
                byte[] bytes;
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    bytes = Encoding.ASCII.GetBytes(line);
                    s.Send(bytes);
                    Thread.Sleep((1 / this.speed) * 100);
                }
                Console.WriteLine("end");
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
