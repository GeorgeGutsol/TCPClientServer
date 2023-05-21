using Messages;
using Messages.Handlers;
using Messages.Protobuf;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
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

        public void Stop()
        {
            foreach (var client in _clients.Values)
            {
                client.Disconnect();
                client.Dispose();
            }
        }
        /// <summary>
        /// Обрабатывает полученные сообщения
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="timedOut"></param>
        private void HandleMessage(object obj = null, bool timedOut = false)
        {
            _recievedBytes.TryDequeue(out byte[] data);

            switch (MessageHandler.Parse(data))
            {
                case SymbolMessage symbolMessage:
                    HandleSymbolMessage(symbolMessage);
                    break;
                case ServiceMessage serviceMessage:
                    HandleDisconnectMessage(serviceMessage);
                    break;
            }

            void HandleSymbolMessage(SymbolMessage symbolMessage)
            {
                Log.RecieveLog(symbolMessage);
                if (!_clients.TryGetValue(symbolMessage.ClientId, out var client)) return;
                WriteWaitHandler writeHandler = new WriteWaitHandler
                {
                    Client = client,
                    Message = MessageHandler.Serialize(symbolMessage)
                };
                writeHandler.CreateWaitHandler();
            }

            void HandleDisconnectMessage(ServiceMessage serviceMessage)
            {
                Log.RecieveLog(serviceMessage);
                if (!_clients.TryGetValue(serviceMessage.ClientId, out var client)) return;
                if (serviceMessage.Operation == OperationType.Disconnect)
                {
                    client.Disconnect();
                    client.Dispose();
                    _clients.TryRemove(serviceMessage.ClientId, out client);
                }
            }
        }

        /// <summary>
        /// Инициализирует клиентсикй поток на чтение
        /// </summary>
        /// <param name="client"></param>
        private void InitClientThread(Client client)
        {
            Thread thread = new Thread(() =>
            {
                SendServiceMessage(client, OperationType.New);
                _clients.TryAdd(client.Id, client);
                Thread.Sleep(10);//небольшая задержка, чтобы клиент успел получить сервисное сообщение
                InitClientTimer(client);

                while (!client.Token.IsCancellationRequested)
                {
                    var message = client.Read();
                    if (message.Length == 0) break;
                    EnqueueMessage(message);
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Отправляет текущее время и инициализирует таймер для отправки времени каждые 10 секунд
        /// </summary>
        /// <param name="client"></param>
        private void InitClientTimer(Client client)
        {
            client.Timer = new System.Timers.Timer(10000);
            client.Timer.Elapsed += (o, e) =>
            {
                CreateWriteTimeHandler();
            };
            CreateWriteTimeHandler();
            client.Timer.Start();

            void CreateWriteTimeHandler()
            {
                var writeWaitHandler = new WriteWaitHandler();
                writeWaitHandler.Client = client;
                writeWaitHandler.CreateWaitHandler(WriteTime);
            }

            //Определяет время и тут же отправляет его клиенту
            void WriteTime(object obj, bool elapsed)
            {
                if (obj is WriteWaitHandler writeHandler)
                {
                    var message = new DateTimeMessage()
                    {
                        ClientId = writeHandler.Client.Id,
                        DateTimeOffset = DateTimeOffset.Now
                    };
                    writeHandler.Message = MessageHandler.Serialize(message);
                    writeHandler.Write(writeHandler, elapsed);
                    Log.SendLog(message);
                }
            }
        }

        private void SendServiceMessage(Client client, OperationType operationType)
        {
            client.Semaphore.WaitOne();
            var message = new ServiceMessage() { ClientId = client.Id, Operation = operationType };
            client.SafeWrite(MessageHandler.Serialize(message));
            Log.SendLog(message);
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
