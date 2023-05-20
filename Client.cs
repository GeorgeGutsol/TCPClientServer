using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Client : IDisposable
    {

        public TcpClient TcpClient { get; private set; }

        public CancellationTokenSource Token { get; private set; } = new CancellationTokenSource();

        public Semaphore Semaphore { get; private set; } = new Semaphore(1, 1);

        public System.Timers.Timer Timer { get; set; }

        public int Id { get; private set; } = -1;

        public Client(TcpClient client, int clientId)
        {
            Id = clientId;
            TcpClient = client;
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
                int readed = TcpClient.GetStream().Read(readBuffer, 0, readBuffer.Length);
                if (readed == 0) Disconnect();
                Array.Resize(ref readBuffer, readed);
                return readBuffer;
            }
            catch (IOException e)
            {
                if (e.InnerException is SocketException)
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
            try
            {
                TcpClient.GetStream().Write(message, 0, message.Length);
            }
            catch (Exception e)
            {
                if (e.InnerException is SocketException)
                {
                    Disconnect();
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public void Disconnect()
        {
            Console.WriteLine($"Client ID {Id} disconnected");

            Timer.Stop();

            Semaphore?.WaitOne();
            Semaphore?.Close();

            Token?.Cancel();
            TcpClient?.Close();
        }

        public void Dispose()
        {
            Token?.Dispose();
            Timer?.Dispose();
        }

        public void Reconnect(Client client)
        {
            TcpClient = client.TcpClient;
            Semaphore = client.Semaphore;
            Token.Dispose();
            Token = client.Token;
        }

        public void SetSocketKeepAliveValues(bool On, int KeepAliveTime, int KeepAliveInterval)
        {
            byte[] inOptionValues = new byte[sizeof(uint) * 3];

            BitConverter.GetBytes((uint)(On ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)KeepAliveTime).CopyTo(inOptionValues, sizeof(uint));
            BitConverter.GetBytes((uint)KeepAliveInterval).CopyTo(inOptionValues, sizeof(uint) * 2);

            TcpClient.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }
    }
}
