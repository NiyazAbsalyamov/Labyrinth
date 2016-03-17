using System;
using Labyrinth.Services;

namespace ConsoleTest
{
    class ConsoleTest
    {
        static void Main()
        {
            var commandCreatorService = new CommandCreatorService();

            commandCreatorService.BeginConnection("127.0.0.1", "1000");

            if (commandCreatorService.CheckConnection())
            {
                Console.Write(@"Норм");
            }

            commandCreatorService.Send("123");
            var k = commandCreatorService.Receive();
            commandCreatorService.EndConnection();
            Console.Write(k);

            foreach (var exception in Labyrinth.ExceptionLogger.ExceptionLogger.ExceptionsList)
            {
                Console.Write(exception + '\n');
            }
        }
    }
}
