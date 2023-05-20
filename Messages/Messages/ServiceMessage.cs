using Messages.Protobuf;

namespace Messages
{
    public class ServiceMessage : BaseMessage
    {
        public OperationType Operation { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ServiceMessage message)
            {
                if (message.Operation == this.Operation && message.ClientId == this.ClientId)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Client {ClientId} operation {Operation}";
        }
    }
}
