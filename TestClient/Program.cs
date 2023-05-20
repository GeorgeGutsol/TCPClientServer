using Messages;
using Messages.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TestClient
{
    internal class Client
    {
        public TcpClient TcpClient { get; } = new TcpClient();
        public long elapsedTime { get; set; } = 0;

        public long nBytes { get; set; } = 0;

        public int ID = -1;
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            SymbolSender();

        }

        private static void SpeedTest()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            int clientCount = 1;
            var clients = new List<Client>();

            object locker = new object();
            for (int i = 0; i < clientCount; i++)
            {
                var client = new Client();
                client.TcpClient.Connect(IPAddress.Parse("127.0.0.1"), 5201);
                clients.Add(client);
                Thread thread = new Thread(() =>
                {
                    RandomSend(cancellationToken, client);
                }
                );
                thread.Start();
            }
            Console.WriteLine("To end press Enter");
            Console.ReadLine();
            cancellationToken.Cancel();
            Thread.Sleep(200);
            for (int i = 0; i < clientCount; i++)
            {
                clients[i].TcpClient.Close();
                Console.WriteLine($"Client {i}: {clients[i].nBytes} bytes/{clients[i].elapsedTime} ms");

            }
            Console.ReadLine();
            cancellationToken.Dispose();
        }

        private static void SetSocketKeepAliveValues(TcpClient tcpClient, bool On, int KeepAliveTime, int KeepAliveInterval)
        {

            byte[] inOptionValues = new byte[sizeof(uint) * 3];

            BitConverter.GetBytes((uint)(On ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)KeepAliveTime).CopyTo(inOptionValues, sizeof(uint));
            BitConverter.GetBytes((uint)KeepAliveInterval).CopyTo(inOptionValues, sizeof(uint) * 2);

            tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }



        private static void SymbolSender()
        {
            var client = new Client();
            SetSocketKeepAliveValues(client.TcpClient, true, 1000, 1000);
            client.TcpClient.Connect(IPAddress.Parse("127.0.0.1"), 5201);

            ProtobufHandler protobufHandler = new ProtobufHandler();
            ReadDataAsync(client, protobufHandler);
            string s;
            do
            {
                s = Console.ReadLine();
                SymbolMessage symbolMessage = new SymbolMessage()
                {
                    Symbol = s,
                    ClientId = client.ID
                };
                byte[] buffer = protobufHandler.Serialize(symbolMessage);
                client.TcpClient.GetStream().Write(buffer, 0, buffer.Length);
            }
            while (!string.IsNullOrEmpty(s));
            client.TcpClient.Close();
        }

        private static void CreateReadThread(Client client, ProtobufHandler protobufHandler)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    var buffer = new byte[client.TcpClient.ReceiveBufferSize];
                    int readed = client.TcpClient.GetStream().Read(buffer, 0, buffer.Length);
                    if (readed == 0) break;
                    Array.Resize(ref buffer, readed);
                    switch (protobufHandler.Parse(buffer))
                    {
                        case SymbolMessage symbolMessage:
                            Console.WriteLine($"Server respond:{symbolMessage.Symbol} for client {symbolMessage.ClientId} ");
                            break;
                        case ServiceMessage serviceMessage:
                            Console.WriteLine($"Server send service info:{serviceMessage.Operation} for client {serviceMessage.ClientId} ");
                            client.ID = serviceMessage.ClientId;
                            break;
                        case DateTimeMessage serviceMessage:
                            Console.WriteLine($"Server send service info:{serviceMessage.DateTimeOffset} for client {serviceMessage.ClientId} ");
                            break;
                    }
                }


            });
            thread.Start();
        }

        private static async void ReadDataAsync(Client client, ProtobufHandler protobufHandler)
        {

            while (true)
            {
                var buffer = new byte[client.TcpClient.ReceiveBufferSize];
                int readed = await client.TcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
                if (readed == 0) break;
                Array.Resize(ref buffer, readed);
                switch (protobufHandler.Parse(buffer))
                {
                    case SymbolMessage symbolMessage:
                        Console.WriteLine($"Server respond:{symbolMessage.Symbol} for client {symbolMessage.ClientId} ");
                        break;
                    case ServiceMessage serviceMessage:
                        Console.WriteLine($"Server send service info:{serviceMessage.Operation} for client {serviceMessage.ClientId} ");
                        client.ID = serviceMessage.ClientId;
                        break;
                    case DateTimeMessage serviceMessage:
                        Console.WriteLine($"Server send service info:{serviceMessage.DateTimeOffset} for client {serviceMessage.ClientId} ");
                        break;
                }
            }
        }

        private static void RandomSend(CancellationTokenSource cancellationToken, Client client)
        {
            using (var stream = client.TcpClient.GetStream())
            {
                Random random = new Random();
                byte[] buffer = new byte[1000];

                byte[] data = new byte[2000];
                long n = 0;

                var stopwatch = new Stopwatch();
                while (!cancellationToken.IsCancellationRequested)
                {
                    n++;
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        buffer[j] = (byte)random.Next(65, 90);
                    }
                    stopwatch.Start();
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Read(data, 0, data.Length);
                    stopwatch.Stop();
                    Console.WriteLine(Encoding.UTF8.GetString(data));
                    Thread.Sleep(50);
                }
                client.elapsedTime = stopwatch.ElapsedMilliseconds;
                client.nBytes = n;
            }
        }
    }
}
