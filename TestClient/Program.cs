﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Messages;
using Messages.Handlers;

namespace TestClient
{
    internal class Client
    {
        public TcpClient tcpClient { get; } = new TcpClient();
        public long elapsedTime { get; set; } = 0;

        public long nBytes { get; set; } = 0;
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
                client.tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 5201);
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
                clients[i].tcpClient.Close();
                Console.WriteLine($"Client {i}: {clients[i].nBytes} bytes/{clients[i].elapsedTime} ms");

            }
            Console.ReadLine();
            cancellationToken.Dispose();
        }

        private static void SymbolSender()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 5201);
            ProtobufHandler protobufHandler = new ProtobufHandler();
            Thread thread = new Thread(()=>
            {
                while (true)
                {
                    var buffer = new byte[tcpClient.ReceiveBufferSize];
                    int readed = tcpClient.GetStream().Read(buffer, 0, buffer.Length);
                    if (readed == 0) break;
                    Array.Resize(ref buffer, readed);
                    Console.WriteLine($"Server respond:{((SymbolMessage)protobufHandler.Parse(buffer)).Symbol} ");
                }
               

            });
            thread.Start();
            string s;
                do
                {
                    s = Console.ReadLine();
                    SymbolMessage symbolMessage = new SymbolMessage()
                    {
                        Symbol = s,
                        ClientId = -1
                    };
                    byte[] buffer = protobufHandler.Serialize(symbolMessage);
                    tcpClient.GetStream().Write(buffer, 0, buffer.Length);
                }
                while (!string.IsNullOrEmpty(s));
            tcpClient.Close();
        }

        private static void RandomSend(CancellationTokenSource cancellationToken, Client client)
        {
            using (var stream = client.tcpClient.GetStream())
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
