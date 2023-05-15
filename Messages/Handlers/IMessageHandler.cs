using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Handlers
{
    public interface IMessageHandler
    {
        BaseMessage Parse(byte[] input);
        byte[] Serialize(BaseMessage message);
    }
}
