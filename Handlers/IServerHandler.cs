using Messages.Handlers;
using System.Net.Sockets;

namespace Server.Handlers
{
    public interface IServerHandler
    {
        IMessageHandler MessageHandler { get; set; }
        void Handle(TcpClient client);

        void Stop();
    }
}
