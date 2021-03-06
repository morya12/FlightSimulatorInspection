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
    public class ConnectionHandler : BaseModel
    {
        public bool running;
        public double speed;
        private int timeStep;
        public int TimeStep
        {
            get
            {
                return timeStep;
            }
            set
            {
                if (timeStep != value)
                {
                    timeStep = value;
                    NotifyPropertyChanged(nameof(TimeStep));
                }
            }
        }
        public double Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public ConnectionHandler()
        {
            running = false;
        }
        public bool Running
        {
            get
            {
                return running;
            }
            set
            {
                running = value;
            }
        }

        public void handle(string[] csvLines, Socket socket)
        {
            byte[] bytes;
            for (TimeStep = 1; TimeStep < csvLines.Length; TimeStep++)
            {
                if (!Running)
                    DataBase.mre.WaitOne();
                bytes = Encoding.ASCII.GetBytes(csvLines[TimeStep] + "\r\n");
                try
                {
                    socket.Send(bytes);
                    FlightStats.Instance.updateStatsTable(csvLines[TimeStep]);
                    Thread.Sleep((int)Speed);
                }
                catch
                {
                    break;
                }
            }
            Running = false;
        }
    }
}
