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
                Client.SafeWrite(Message);
                RegisteredWaitHandle.Unregister(null);
        }
        /// <summary>
        /// Регистрирует ожидание для операции записи методом Write по умолчанию
        /// </summary>
        public void CreateWaitHandler()
        {
            RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(Client.Semaphore,
                   Write,
                    this,
                      -1,
                    true);
        }
        /// <summary>
        /// Регистрирует ожидание для операции записи
        /// </summary>
        /// <param name="writeWaitCallback">Custom метод Write</param>
        public void CreateWaitHandler(WaitOrTimerCallback writeWaitCallback)
        {
            RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(Client.Semaphore,
                   writeWaitCallback,
                    this,
                      -1,
                    true);
        }
    }
}
