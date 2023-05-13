using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtobufMessages;
using Google.Protobuf;

namespace Messages.Handlers
{
    public class ProtobufHandler : IHandler
    {
        public BaseMessage Parse(byte[] input)
        {
            
            var data = Data.Parser.ParseFrom(input);
            switch (data.ValueCase)
            {
                case Data.ValueOneofCase.Symbol:
                    return new SymbolMessage()
                    {
                        clientId = data.Id,
                        Symbol = data.Symbol
                    };
                case Data.ValueOneofCase.ServiceInfo:
                    return new ServiceMessage()
                    {
                        clientId = data.Id,
                        operation = data.ServiceInfo.Operation
                    };
                default:
                    return new BaseMessage()
                    {
                        clientId = data.Id
                    };
                       
            }
                
        }

        public byte[] Serialize(BaseMessage message)
        {
            Data data = new Data()
            {
                Id = message.clientId
            };
            switch (message)
            {
                case ServiceMessage serviceMessage:
                    data.ServiceInfo.Operation = serviceMessage.operation;
                    return WriteData(data);
                case SymbolMessage symbolMessage:
                    data.Symbol = symbolMessage.Symbol;
                    return WriteData(data);
            }
            return WriteData(data);
        }

        private byte[] WriteData(Data data)
        {
            byte[] buffer = new byte[data.CalculateSize()];
            using (CodedOutputStream codedOutput = new CodedOutputStream(buffer))
            {
                data.WriteTo(codedOutput);
            }
            return buffer;
        }
    }
}
