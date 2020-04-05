using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separador
{
    class Menu
    {
        private static void Display_menu( string[] items)
        {
            Console.WriteLine("Choose menu item:");
            int i = 0;
            foreach(string item in items)
            {
                Console.WriteLine($"{i + 1}) {item}");
                i++;
            }
        }
    }
}
