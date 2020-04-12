using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    class Friend
    {
        private string name;
        private List<Dish> dishes;
 
        public string Name
        {
            set
            {
                name = value ?? "Unnamed";
                if (name.Length > 1)
                    name = char.ToUpper(name[0]) + name.Substring(1);

            }
            get
            {
                return name;
            }
        }
        
        public void Add_dish()
        {
            Console.WriteLine("Input name of dish");
            string name = Console.ReadLine();
            Console.WriteLine("Input cost");
            int cost = int.Parse(Console.ReadLine());
            dishes.Add(new Dish(name, cost));

        }
        public void Add_dish(Dish dish)
        {
            dishes.Add(dish);
        }
        public void Print_all_dishes() { 
        
            foreach(Dish dish in dishes)
            {
                Console.WriteLine(dish);
            }
        }

        public Friend() : this("Unnamed") { }
        public Friend(string name)
        {
            Name = name;
            dishes = new List<Dish>();
        }

    }
}
