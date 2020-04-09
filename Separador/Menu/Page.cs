using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator.Menu
{
    public class Page
    {
        private IList<Option> Options { get; set; }

        public Page()
        {
            Options = new List<Option>();
        }

        public void Display()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
            }
            string input = Console.ReadLine();
            int choice;

            while (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter an integer");
                input = Console.ReadLine();
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
