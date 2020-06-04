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
                Console.WriteLine($"{i + 1}. { Options[i].Name}");
            }
            
            int choice = Input.ReadInt("Choose an option:", min: 1, max: Options.Count);

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
