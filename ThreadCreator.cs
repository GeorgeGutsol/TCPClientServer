using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    internal class ThreadCreator : IClientThreadCreator
    {

        public void CloseThread(Client client)
        {
            client.Disconnect();
        }

        public void CreateThread(Client client)
        {
            Thread thread = new Thread(client.Read);
            thread.Start();
        }
    }
}
