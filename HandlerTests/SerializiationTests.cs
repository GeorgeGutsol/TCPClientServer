using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messages;
using Messages.Handlers;
using System;

namespace HandlerTests
{
    [TestClass]
    public class SerializiationTests
    {
        [TestMethod]
        public void TestSymbolMethod()
        {
            SymbolMessage message = new SymbolMessage()
            {
                clientId = 1,
                Symbol = "a"
            };
            ProtobufHandler protobufHandler = new ProtobufHandler();
            var serialized = protobufHandler.Serialize(message);
            var deserializedMessage = (SymbolMessage)protobufHandler.Parse(serialized);
            Assert.AreEqual(message.clientId,deserializedMessage.clientId);
            Assert.AreEqual(message.Symbol,deserializedMessage.Symbol);
        }
    }
}
