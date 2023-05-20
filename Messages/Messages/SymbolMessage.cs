namespace Messages
{
    public class SymbolMessage : BaseMessage
    {
        public string Symbol { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SymbolMessage message)
            {
                if (message.Symbol == this.Symbol && message.ClientId == this.ClientId)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Client {ClientId} symbol {Symbol}";
        }
    }
}
