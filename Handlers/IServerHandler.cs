using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.Handlers;
using System.Net.Sockets;

namespace Server.Handlers
{
    public interface IServerHandler
    {
        IMessageHandler MessageHandler { get; set; }
        void Handle(TcpClient client);
    }
}
