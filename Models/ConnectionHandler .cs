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
        public int speed;
        private int timeStep;
        //Thread handlingThread = new Thread(new ThreadStart(this));
        //public Thread HandlingThread
        //{
        //    get
        //    {
        //        return handlingThread;
        //    }
        //}
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
        //socket not working.. need to fix it than we can change function signature
        //public void handle(string[] csvLines, Socket socket)
        public void handle(string[] csvLines)
        {
            byte[] bytes;
            //ThreadStart start = delegate () { };
            //Thread t = new Thread((){ }).Start();

            new Thread(delegate ()
            {
                if (!Running)
                    Thread.Sleep(Timeout.Infinite);
                for (TimeStep = 1; TimeStep < csvLines.Length; TimeStep++)
                {
                    if (Running)
                    {
                        bytes = Encoding.ASCII.GetBytes(csvLines[TimeStep]);
                        //socket.Send(bytes);
                        FlightStats.Instance.updateStatsTable(csvLines[TimeStep]);
                        Thread.Sleep(10 / 1);

                    }
                }

                //socket.Disconnect(true);

            }).Start();
        }
    }
}
