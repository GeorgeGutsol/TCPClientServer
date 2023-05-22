using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Messages.Handlers;
using Messages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Nito.AsyncEx;
using System.Collections.ObjectModel;

namespace WPFClient
{
    public class Client : INotifyPropertyChanged
    {


        public string MessageLog { get { return _messageLog.ToString(); } }

        public string ConnectionLog { get { return _connectionLog.ToString(); } }

        public string IP
        {
            get => iP;
            set
            {
                iP = value;
                OnPropertyChanged();
            }
        }
        public string Port
        {
            get => port;
            set
            {
                port = value;
                OnPropertyChanged();
            }
        }


        public string Symbols
        {
            get { return _symbols; }
            set
            {
                _symbols = value;
                OnPropertyChanged();
                WriteMessageAsync(_symbols);
                _symbols = string.Empty;
            }
        }
        /// <summary>
        /// Клиент находиться в подключенном сотоянии или пытается подключиться
        /// </summary>
        public bool IsConnected 
        { 
            get => isConnected;
            set
            {
                isConnected = value;
                ChangeConnectionState();
                OnPropertyChanged();
            }
        }

        public async Task ChangeConnectionState()
        {
            if (isConnected)
            {
                _tcpClient = new TcpClient();
                SetSocketKeepAliveValues(true, 5, 1);
                await ConnectAsync();
            }
            else
            {
                if (_tcpClient.Connected)
                {
                    InsertConnectionLog("Closed connection to the server");
                }
                _tcpClient.Close();
            }

        }
        private const int maxRequests = 120;
        private int requests = maxRequests;
        /// <summary>
        /// Пытается установить соединение maxRequest раз
        /// </summary>
        /// <returns></returns>
        private async Task ConnectAsync()
        {
            try
            {
                InsertConnectionLog("Trying to connect to the server");
                if (!IsConnected) return;
                await _tcpClient.ConnectAsync(IPAddress.Parse(IP), int.Parse(Port));
                StartLogLoopAsync();
                StartReadLoopAsync();
                requests = maxRequests;
                InsertConnectionLog("Connected to the server");
            }
            catch (SocketException)
            {
                if (--requests > 0)
                {
                    await Task.Delay(1000);
                    ConnectAsync();
                }
                else
                {
                    requests = maxRequests;
                    IsConnected = false;
                    InsertConnectionLog("Server is not richable");
                }
            }
        }

        private async Task<byte[]> ReadDataAsync()
        {
            try
            {
                var buffer = new byte[_tcpClient.ReceiveBufferSize];
                int readed = await _tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
                if (readed == 0)
                {
                    Reconnect();
                    return new byte[0];
                }
                Array.Resize(ref buffer, readed);
                return buffer;
            }
            catch (IOException e)
            {
                if (e.InnerException is SocketException)
                {
                    Reconnect();
                }
                else
                {
                    throw;
                }
                return new byte[0];
            }

        }

        private void Reconnect()
        {
            LogDisconnect();
            _tcpClient.Client.Disconnect(true);
            ConnectAsync();
        }

        private void InsertConnectionLog(string message)
        {
            _connectionLog.Insert(0, "\r\n");
            _connectionLog.Insert(0, message);
            _connectionLog.Insert(0, $"{DateTime.Now} Client {ID}:  ");
            OnPropertyChanged(nameof(ConnectionLog));
        }

        private void LogDisconnect()
        {
            InsertConnectionLog("Lost connection to the server");
        }


        ProtobufHandler protobufHandler = new ProtobufHandler();
        private async Task StartReadLoopAsync()
        {
            while (_tcpClient.Connected)
            {
                var data = await ReadDataAsync();
                if (data.Length == 0) break;
                _messageQueue.Enqueue(data);
            }
        }

        private async Task StartLogLoopAsync()
        {
            while (_tcpClient.Connected)
            {
                var data = await _messageQueue.DequeueAsync();
                LogMessage(data);
            }
        }


        

        private void LogMessage(byte[] data)
        {
            if (_messageLog.Length > 5000) _messageLog.Remove(5000, _messageLog.Length - 5000);
            _messageLog.Insert(0, "\r\n");
            try
            {
                var message = protobufHandler.Parse(data);
                _messageLog.Insert(0, message);
                if (message is ServiceMessage serviceMessage)
                {
                    if (serviceMessage.Operation == Messages.Protobuf.OperationType.New)
                    {
                        ID = serviceMessage.ClientId;
                        InsertConnectionLog($"My new ID {ID}");
                    }
                }
            }
            catch (Exception e)
            {
                _messageLog.Insert(0, $"Incorrect message: {e.Message}");
            }
            finally
            {
                OnPropertyChanged(nameof(MessageLog));
            }
            
        }
            

        private async Task WriteMessageAsync(string message)
        {
            if (_tcpClient==null && !_tcpClient.Connected) return;
            SymbolMessage symbolMessage = new SymbolMessage()
            {
                ClientId = ID,
                Symbol = message
            };

            var buffer = protobufHandler.Serialize(symbolMessage);

            _tcpClient.GetStream().WriteAsync(buffer, 0, buffer.Length);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private TcpClient _tcpClient;

        private int ID = -1;

        private string _symbols;

        private StringBuilder _messageLog = new StringBuilder();
        private StringBuilder _connectionLog = new StringBuilder();
        private bool isConnected = false;

        private string iP = "127.0.0.1";
        private string port = "5201";

        private AsyncProducerConsumerQueue<byte[]> _messageQueue = new AsyncProducerConsumerQueue<byte[]>();

        private void SetSocketKeepAliveValues(bool On, int KeepAliveTime, int KeepAliveInterval)
        {
            byte[] inOptionValues = new byte[sizeof(uint) * 3];

            BitConverter.GetBytes((uint)(On ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)KeepAliveTime).CopyTo(inOptionValues, sizeof(uint));
            BitConverter.GetBytes((uint)KeepAliveInterval).CopyTo(inOptionValues, sizeof(uint) * 2);

            _tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }

    }
}
