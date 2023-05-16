using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Client:IDisposable
    {

        private TcpClient _client;

        public CancellationTokenSource Token { get; } = new CancellationTokenSource();

        public Semaphore Semaphore { get; } = new Semaphore(1, 1);

        public int Id { get; private set; } = -1;

        public Client(TcpClient client, int clientId)
        {
            Id = clientId;
            _client = client;
        }

        /// <summary>
        /// Читает данные с помощью NetworkStream
        /// </summary>
        /// <returns>Прочитанный массив байт, размер соответствует количеству прочитанных байт(0, если соединение разорвано).</returns>
        public byte[] Read()
        {
            byte[] readBuffer = new byte[8192];
            try
            {
                int readed = _client.GetStream().Read(readBuffer, 0, readBuffer.Length);
                if (readed == 0) Disconnect();
                Array.Resize(ref readBuffer, readed);
                return readBuffer;
            }
            catch (IOException e)
            {
                if (e.InnerException is SocketException socketException)
                {
                    Disconnect();
                }
                else
                {
                    throw;
                }
                return new byte[0];
            }
        }
        /// <summary>
        /// Безопасный к потокам метод записи в NetworkStream, вызывающий метод должен позаботиться о входе в Client.Semaphore 
        /// </summary>
        /// <param name="message"></param>
        public void SafeWrite(byte[] message)
        {
            _client.GetStream().Write(message, 0, message.Length);
            Semaphore.Release();
        }

        public void Disconnect()
        {
            Semaphore.WaitOne();
            Console.WriteLine($"Client ID {Id} disconnected");
            Token?.Cancel();
            _client.Close();
            
        }

        public void Dispose()
        {
            Token?.Dispose();
        }
    }
}
