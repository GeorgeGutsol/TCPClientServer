using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server.Handlers
{
    /// <summary>
    ///Класс для регистрации ожидания на запись сообщения
    /// </summary>
    internal class WriteWaitHandler
    {
        public Client Client { get; set; }
        public byte[] Message { get; set; }
        public RegisteredWaitHandle RegisteredWaitHandle { get; set; }

        static public void Write(object obj, bool timeOut)
        {
            if (obj is WriteWaitHandler writeHandler)
            {
                writeHandler.Client.Write(writeHandler.Message);
                writeHandler.RegisteredWaitHandle.Unregister(null);
            }
        }
    }
}
