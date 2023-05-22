using Server.Handlers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class Server
    {

        public Server(IPAddress adress, int port, IServerHandler serverHandler)
        {
            _listener = new TcpListener(adress, port);
            handler = serverHandler;
        }

        public CancellationTokenSource token { get; } = new CancellationTokenSource();

        public void StartListen()
        {
            _listener.Start();
            Console.WriteLine("ServerStarted");
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var tcpClient = _listener.AcceptTcpClient();
                    handler.Handle(tcpClient);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Слушатель завершил свою работу");
                }
                catch (SocketException)
                {
                    Console.WriteLine("Ошибка слушающего сокета");
                }
                
               
            }
        }

        public void Close()
        {
            _listener.Stop();
            handler.Stop();
            token.Cancel();
            token.Dispose();
        }

        private TcpListener _listener;
        private IServerHandler handler;

    }
}
