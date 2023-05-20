using Messages;
using Messages.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HandlerTests
{
    [TestClass]
    public class SerializiationTests
    {
        ProtobufHandler protobufHandler = new ProtobufHandler();
        [TestMethod]
        public void TestSymbolMessage()
        {
            SymbolMessage message = new SymbolMessage()
            {
                ClientId = 1,
                Symbol = "a"
            };

            var serialized = protobufHandler.Serialize(message);
            var deserializedMessage = (SymbolMessage)protobufHandler.Parse(serialized);
            Assert.AreEqual(message, deserializedMessage);
        }
        [TestMethod]
        public void TestServiceMessage()
        {
            ServiceMessage message = new ServiceMessage()
            {
                ClientId = 1,
                Operation = Messages.Protobuf.OperationType.New
            };

            var serialized = protobufHandler.Serialize(message);
            var deserializedMessage = (ServiceMessage)protobufHandler.Parse(serialized);
            Assert.AreEqual(message, deserializedMessage);
        }
        [TestMethod]
        public void TestDateTimeMessage()
        {
            DateTimeMessage message = new DateTimeMessage()
            {
                ClientId = 1,
                DateTimeOffset = DateTimeOffset.Now
            };

            var serialized = protobufHandler.Serialize(message);
            var deserializedMessage = (DateTimeMessage)protobufHandler.Parse(serialized);
            Assert.AreEqual(message, deserializedMessage);
        }
    }
}
