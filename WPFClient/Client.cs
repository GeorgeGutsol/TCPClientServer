using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Messages.Handlers;
using Messages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WPFClient
{
    public class Client : INotifyPropertyChanged
    {


        public string MessageLog { get { return _messageLog.ToString(); } }

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
                WriteMessage(_symbols);
                _symbols = string.Empty;
            }
        }

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
                await Connect();
                HandleMessage();
            }
            else
            {
                _tcpClient.Close();
            }

        }

        private int requests = 120;
        private async Task Connect()
        {
            try
            {
                await _tcpClient.ConnectAsync(IPAddress.Parse(IP), int.Parse(Port));
            }
            catch (SocketException)
            {
                if (--requests > 0&&IsConnected)
                {
                    await Task.Delay(1000);
                    Connect();
                }
                else
                {
                    requests = 120;
                    IsConnected = false;
                }
            }
        }

        private async Task<byte[]> ReadDataAsync()
        {
            var buffer = new byte[_tcpClient.ReceiveBufferSize];
            int readed = await _tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
            //if (readed == 0) break;
            Array.Resize(ref buffer, readed);
            return buffer;
        }
        ProtobufHandler protobufHandler = new ProtobufHandler();
        private async Task HandleMessage()
        {
            while (true)
            {
                var data = await ReadDataAsync();
                _messageLog.Insert(0, "\r\n");
                switch (protobufHandler.Parse(data))
                {
                    case SymbolMessage symbolMessage:
                        _messageLog.Insert(0, symbolMessage.ToString());
                        break;
                    case ServiceMessage serviceMessage:
                        _messageLog.Insert(0, serviceMessage.ToString());
                        ID = serviceMessage.ClientId;
                        break;
                    case DateTimeMessage dateTimeMessage:
                        _messageLog.Insert(0, dateTimeMessage.ToString());
                        break;
                }
                OnPropertyChanged(nameof(MessageLog));
            }
        }

        private async Task WriteMessage(string message)
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
        private bool isConnected = false;

        private string iP = "127.0.0.1";
        private string port = "5201";
    }
}
