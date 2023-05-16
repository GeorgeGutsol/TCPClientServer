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

        public void Write(object obj, bool timeOut)
        {
            if (obj is WriteWaitHandler writeHandler)
            {
                writeHandler.Client.SafeWrite(writeHandler.Message);
                writeHandler.RegisteredWaitHandle.Unregister(null);
            }
        }
        /// <summary>
        /// Регистрирует ожидание для операции записи 
        /// </summary>
        public void CreateWaitHandler()
        {
            RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(Client.Semaphore,
                   Write,
                    this,
                      -1,
                    true);
        }
    }
}
