using System.Collections.Generic;

namespace Labyrinth.ExceptionLogger
{
    public static class ExceptionLogger
    {
        public static List<string> ExceptionsList;

        static ExceptionLogger()
        {
            ExceptionsList = new List<string>();
        }

        public static void AddException(string mes)
        {
            ExceptionsList.Add(mes);
        }
    }
}
