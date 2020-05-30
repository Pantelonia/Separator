using System;
namespace Separator
{
    public class MyConsole : IConsole
    {
        public void Clear()
        {
            Console.Clear();
        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
