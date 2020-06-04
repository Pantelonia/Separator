using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    public class Input
    {
        public static IConsole myConsole = new MyConsole();

        public static int ReadInt(string prompt, int min, int max)
        {
            myConsole.WriteLine(prompt);
            return ReadInt(min, max);
        }

        public static int ReadInt(int min, int max)
        {
            int value = ReadInt();

            while (value < min || value > max)
            {
                myConsole.WriteLine($"Please enter an integer between {min} and {max} (inclusive)");
                value = ReadInt();
            }

            return value;
        }

        public static int ReadInt()
        {
            string input = myConsole.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                Console.WriteLine("Please enter an integer");
                input = myConsole.ReadLine();
            }

            return value;
        }

        public static string ReadString(string prompt)
        {
            myConsole.WriteLine(prompt);
            return myConsole.ReadLine();
        }
    }
}
