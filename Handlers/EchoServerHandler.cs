using Messages.Handlers;
using Messages;
using Messages.Protobuf;
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
            Client client = new Client(tcpClient, _clientsCounter++);
            InitClientThread(client);
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
                    HandleSymbolMessage(symbolMessage);
                    break;
            }

            void HandleSymbolMessage(SymbolMessage symbolMessage)
            {
                Console.WriteLine($"ID {symbolMessage.ClientId} send symbol {symbolMessage.Symbol}");
                if (!_clients.TryGetValue(symbolMessage.ClientId, out var client)) return; 
                WriteWaitHandler writeHandler = new WriteWaitHandler
                {
                    Client = client,
                    Message = MessageHandler.Serialize(symbolMessage)
                };
                writeHandler.CreateWaitHandler();
            }
        }

        private void InitClientThread(Client client)
        {
            Thread thread = new Thread(() =>
            {
                client.Semaphore.WaitOne();
                if (_clients.TryAdd(client.Id, client))
                {
                    SendServiceMessage(client, OperationType.New);
                }
                else
                {
                    SendServiceMessage(client, OperationType.Reconnect);
                }

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


        private void SendServiceMessage(Client client, OperationType operationType)
        {
            var message = MessageHandler.Serialize(new ServiceMessage() { ClientId = client.Id, Operation = operationType});
            client.SafeWrite(message);
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
