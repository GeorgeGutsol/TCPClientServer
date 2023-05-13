using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class Client
    {

        private TcpClient _client;

        public CancellationTokenSource Token { get; } = new CancellationTokenSource();

        public int Id { get; set; } = -1;

        public Client(TcpClient client)
        {
            _client = client;
        }

        public byte[] Read()
        {
            byte[] readBuffer = new byte[8192];
            try
            {
                _client.GetStream().Read(readBuffer, 0, readBuffer.Length);
                return readBuffer;
            }
            catch (IOException e)
            {
                if (e.InnerException is SocketException socketException)
                { 
                    Console.WriteLine($"Client ID {Id} disconnected");
                    Disconnect();
                }
                else
                {
                    throw;
                }
                return readBuffer;
            }
        }

        public void Disconnect()
        {
            Token?.Cancel();
            if (_client.Connected)
            {
                _client.Close();
            }
            Token?.Dispose();
        }
    }
}
