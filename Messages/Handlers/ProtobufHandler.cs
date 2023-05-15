using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.Protobuf;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace Messages.Handlers
{
    public class ProtobufHandler : IMessageHandler
    {
        public BaseMessage Parse(byte[] input)
        {
            
            var data = Data.Parser.ParseFrom(input);
            switch (data.ValueCase)
            {
                case Data.ValueOneofCase.Symbol:
                    return new SymbolMessage()
                    {
                        ClientId = data.Id,
                        Symbol = data.Symbol
                    };
                case Data.ValueOneofCase.ServiceInfo:
                    return new ServiceMessage()
                    {
                        ClientId = data.Id,
                        Operation = data.ServiceInfo
                    };
                case Data.ValueOneofCase.Time:
                    return new DateTimeMessage
                    {
                        ClientId = data.Id,
                        DateTimeOffset = data.Time.ToDateTimeOffset()
                    };
                default:
                    return new BaseMessage()
                    {
                        ClientId = data.Id
                    };
                       
            }
                
        }

        public byte[] Serialize(BaseMessage message)
        {
            Data data = new Data()
            {
                Id = message.ClientId
            };
            switch (message)
            {
                case ServiceMessage serviceMessage:
                    data.ServiceInfo = serviceMessage.Operation;
                    break;
                case SymbolMessage symbolMessage:
                    data.Symbol = symbolMessage.Symbol;
                    break;
                case DateTimeMessage dateTimeMessage:
                    data.Time = Timestamp.FromDateTimeOffset(dateTimeMessage.DateTimeOffset);
                    break;
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
