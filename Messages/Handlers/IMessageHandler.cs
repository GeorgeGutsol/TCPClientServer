namespace Messages.Handlers
{
    public interface IMessageHandler
    {
        BaseMessage Parse(byte[] input);
        byte[] Serialize(BaseMessage message);
    }
}
