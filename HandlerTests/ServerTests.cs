using Messages;
using Messages.Handlers;
using Messages.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HandlerTests
{
    internal class Client
    {
        public ProtobufHandler ProtobufHandler { get; set; } = new ProtobufHandler();
        public TcpClient TcpClient { get; } = new TcpClient();
        public int ID = -1;

        public void Connect()
        {
            TcpClient.Connect(IPAddress.Parse("127.0.0.1"), 5201);
        }

        public BaseMessage Read()
        {
            var buffer = new byte[TcpClient.ReceiveBufferSize];
            int readed =TcpClient.GetStream().Read(buffer, 0, buffer.Length);
            if (readed == 0) return new BaseMessage();
            Array.Resize(ref buffer, readed);
            return ProtobufHandler.Parse(buffer);
        }

    }
    
    [TestClass]
    public class ServerTests
    {
        static Client client;
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            client = new Client();
            client.Connect();
        }

        [TestMethod]
        [DataRow(8000)]
        public void TestEcho(int size)
        {
            Random random = new Random();
            byte[] bytes = new byte[size];
            for(int i=0;i<bytes.Length;i++)
            {
                bytes[i] = (byte)random.Next(65, 90);
            }
            SymbolMessage symbolMessage = new SymbolMessage()
            {
                ClientId = client.ID,
                Symbol = Encoding.UTF8.GetString(bytes)
            };
            var protoMessage = client.ProtobufHandler.Serialize(symbolMessage);
            client.TcpClient.GetStream().Write(protoMessage,0,protoMessage.Length);
            var recieved = client.Read();
            if (recieved is SymbolMessage symbol)
            {
                Assert.AreEqual(symbolMessage.Symbol,symbol.Symbol);
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            client.TcpClient.Close();
        }
    }

    
}
