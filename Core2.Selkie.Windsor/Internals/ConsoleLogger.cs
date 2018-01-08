using System;
using System.Diagnostics.CodeAnalysis;

namespace Core2.Selkie.Windsor.Internals
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine("[DEBUG] " + message);
        }

        public void Error(Exception exception,
                          string message)
        {
            Console.WriteLine("[ERROR] " + message + "\n" + exception + "\n" + exception.StackTrace);
        }

        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void Warn(string message)
        {
            Console.WriteLine("[WARN] " + message);
        }
    }
}