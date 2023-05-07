using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    internal class Server
    {
        public Server(IPAddress adress, int port)
        {
            _listener = new TcpListener(adress, port);    
        }

        public void StartListen(IClientThreadCreator threadCreator)
        {
            _started = true;
            _listener.Start();
            Console.WriteLine("ServerStarted");
            while (_started)
            {
                var tcpClient = _listener.AcceptTcpClient();
                Client client = new Client(tcpClient,_clientsCount++,new ByteEchoReader());
                _clients.Add(client);
                Console.WriteLine($"Client created with Id {client.Id}");
                threadCreator.CreateThread(client);
            }
        }
     
        public void Close()
        {
            _started = false;
            foreach(Client client in _clients)
            {
                client.Disconnect();
            }
        }
        int _clientsCount = 0;
        private TcpListener _listener;
        private List<Client> _clients = new List<Client>();
        private bool _started;
    }
}
