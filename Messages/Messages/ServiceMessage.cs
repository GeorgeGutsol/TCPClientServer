using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtobufMessages;

namespace Messages
{
    public class ServiceMessage : BaseMessage
    {
        public ServiceInfo.Types.OperationType operation { get; set; }
    }
}
