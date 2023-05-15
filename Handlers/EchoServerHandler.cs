using Messages.Handlers;
using Messages;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server.Handlers
{
    internal class EchoServerHandler : IServerHandler
    {
        public IMessageHandler MessageHandler { get; set; }

        public EchoServerHandler(IMessageHandler messageHandler)
        {
            this.MessageHandler = messageHandler;
            ThreadPool.RegisterWaitForSingleObject(_event, HandleMessage, null, -1, false);
        }

        public void Handle(TcpClient tcpClient)
        {
            Client client = new Client(tcpClient);
            client.Id = _clientsCounter++;
            _clients.TryAdd(client.Id, client);
            StartReadThread(client);
        }
        /// <summary>
        /// Обрабатывает полученные сообщения
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="timedOut"></param>
        private void HandleMessage(object obj,bool timedOut)
        {
            _recievedBytes.TryDequeue(out byte[] data);
            
            switch (MessageHandler.Parse(data))
            {
                case SymbolMessage symbolMessage:
                    Console.WriteLine($"ID {symbolMessage.ClientId} send symbol {symbolMessage.Symbol}");
                    _clients.TryGetValue(0, out var client);
                    WriteWaitHandler writeHandler = new WriteWaitHandler
                    {
                        Client = client,
                        Message = MessageHandler.Serialize(symbolMessage)
                    };
                    writeHandler.RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(client.semaphore,
                        WriteWaitHandler.Write,
                        writeHandler,
                        -1,
                        true);
                    break;
            }
        }

        private void StartReadThread(Client client)
        {
            Thread thread = new Thread(() =>
            {
                while (!client.Token.IsCancellationRequested)
                {
                    var message = client.Read();
                    if (message.Length == 0) break;
                    EnqueueMessage(message);
                }
                client.Dispose();

            });
            thread.Start();
        }

        private void EnqueueMessage(byte[] message)
        {
            _recievedBytes.Enqueue(message);
            _event.Set();
            
        }
        /// <summary>
        /// Счетчик для формирования Id клиента
        /// </summary>
        private int _clientsCounter = 0;
        private AutoResetEvent _event = new AutoResetEvent(false);
        private ConcurrentDictionary<int, Client> _clients = new ConcurrentDictionary<int, Client>();
        private ConcurrentQueue<byte[]> _recievedBytes = new ConcurrentQueue<byte[]>();
    }
}
