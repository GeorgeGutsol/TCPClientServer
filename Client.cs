using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    internal class Client
    {
        private int _id;

        private TcpClient _client;

        private INetworkReader _networkReader;

        public CancellationTokenSource token { get; } = new CancellationTokenSource();

        public int Id => _id;

        public Client(TcpClient client, int id, INetworkReader networkReader)
        {
            _client = client;
            _id = id;  
            _networkReader = networkReader;
        }

        public void Read()
        {
            using (var stream = _client.GetStream())
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        _networkReader.Read(stream);
                    }
                    catch (SocketException)
                    {
                        Disconnect();
                    }
               
                }
            }
        }

        public void Disconnect()
        {            
            token?.Cancel();
            if (_client.Connected)
            _client.Close();
            token.Dispose();
        }
    }
}
