using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator.Menu
{
    public class Page
    {
        private IConsole myConsole;
        private IList<Option> Options { get; set; }

        public Page(IConsole console)
        {
            myConsole = console;
            Options = new List<Option>();
        }
        public Page()
        {
            myConsole = new MyConsole();
            Options = new List<Option>();
        }

        public void Display()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                myConsole.WriteLine($"{i + 1}. { Options[i].Name}");
            }
            string input = myConsole.ReadLine();
            int choice;

            while (!int.TryParse(input, out choice))
            {
                myConsole.WriteLine("Please enter an integer");
                input = myConsole.ReadLine();
            }

            Options[choice - 1].Callback();
        }

        public Page Add(string option, Action callback)
        {
            return Add(new Option(option, callback));
        }

        public Page Add(Option option)
        {
            Options.Add(option);
            return this;
        }
    }
}
