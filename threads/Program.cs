using System;
using System.Threading;

namespace practic1
{
    class Program
    {
        static void Main(string[] arr)
        {
            Console.WriteLine("Приложение запущено.");
            
            var command = "";
            string args = "";
            string[] array;
            
            while (true)
            {
                Console.WriteLine("Введите текст запроса для отправки. Для выхода введите /exit");
                command = Console.ReadLine();
                if (command == "/exit") break;
                Console.WriteLine($"Будет послано сообщение '{command}'");
                
                string arg = "";
                Console.WriteLine("Введите аргумент сообщения. Для окончания добавления аргументов введите /end");


                while ((arg = Console.ReadLine()) != "/end")
                {
                    Console.WriteLine("Введите следующий аргумент сообщения. " +
                                      "Для окончания добавления аргументов введите /end");
                    args += " " + arg;

                }

                array = args.Split();
                string id = Guid.NewGuid().ToString("D");
                ThreadPool.QueueUserWorkItem(callBack => MessageSender(command, array, id));
                Console.WriteLine($"Было отправлено сообщение '{command}'. Присвоен идентификатор {id}");

            }
            Console.WriteLine("Приложение завершает работу...");
        }
        static void MessageSender(string msg, string[] args, string id)
        {
            var requestHandler = new DummyRequestHandler();

            try
            {
                Console.WriteLine($"Было отправлено сообщение '{msg}'. " +
                                  $"Получило ответ {requestHandler.HandleRequest(msg, args)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сообщение с идентификатором {id} упало с ошибкой: {ex.Message}");
            }
        }
    }

}