using System;

namespace Messages
{
    public class DateTimeMessage : BaseMessage
    {
        public DateTimeOffset DateTimeOffset { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is DateTimeMessage message)
            {
                if (message.DateTimeOffset.Equals(this.DateTimeOffset) && message.ClientId == this.ClientId)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Client {ClientId} time {DateTimeOffset}";
        }
    }
}
