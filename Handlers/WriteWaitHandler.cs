using System;
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
            try
            {
                Client.SafeWrite(Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }
            finally
            {
                RegisteredWaitHandle.Unregister(null);
            }
        }
        /// <summary>
        /// Регистрирует ожидание для операции записи методом Write по умолчанию
        /// </summary>
        public void CreateWaitHandler()
        {
            try
            {
                RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(Client.Semaphore,
               Write,
                this,
                  -1,
                true);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine($"Client semaphore disposed before wait registration");
            }

        }
        /// <summary>
        /// Регистрирует ожидание для операции записи
        /// </summary>
        /// <param name="writeWaitCallback">Custom метод Write</param>
        public void CreateWaitHandler(WaitOrTimerCallback writeWaitCallback)
        {
            try
            {
                RegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(Client.Semaphore,
                   writeWaitCallback,
                    this,
                      -1,
                    true);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine($"Client semaphore disposed before wait registration");
            }
        }
    }
}
