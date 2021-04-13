using System;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulatorInspection.Models
{
    class ClientFG
    {
        public int host = 5400;
        public Socket connect()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, this.host);
            Console.WriteLine(remoteEP.ToString());
            s.Connect(remoteEP);
            return s;
        }
    }
}
