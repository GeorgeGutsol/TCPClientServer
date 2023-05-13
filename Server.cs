using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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

        public CancellationTokenSource token { get; } = new CancellationTokenSource();

        public void StartListen()
        {
            _listener.Start();
            Console.WriteLine("ServerStarted");
            while (!token.IsCancellationRequested)
            {
                var tcpClient = _listener.AcceptTcpClient();
                CreateClient(tcpClient);
            }
        }

        private void CreateClient(TcpClient tcpClient)
        {
            Client client = new Client(tcpClient);
            _clients.TryAdd(_clientsCounter,client);
            StartReadThread(client);
        }

        private void StartReadThread(Client client)
        {
            Thread thread = new Thread(()=>
            {
                while(!client.Token.IsCancellationRequested)
                {
                    _recievedBytes.Enqueue(client.Read());
                }
            });
            thread.Start();
        }
     
        public void Close()
        {
          token.Cancel();
        }
        int _clientsCounter = 0;
        private TcpListener _listener;
        private ConcurrentDictionary<int,Client> _clients = new ConcurrentDictionary<int,Client>();
        private ConcurrentQueue<byte[]> _recievedBytes = new ConcurrentQueue<byte[]>();
    }
}
