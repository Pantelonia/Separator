using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Separator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Separator\n It is console app that can help you split the bill with your friend\n");
            Menu.Menu welcome = new Menu.Menu();
            welcome.Add("Test menu1", () => Console.WriteLine("TEst1"));
            welcome.Add("Test menu2", () => Console.WriteLine("TEst2"));
            welcome.Add("Test menu3", () => Console.WriteLine("TEst3"));
            welcome.Display();
            Console.ReadKey();

        }
    }
}
