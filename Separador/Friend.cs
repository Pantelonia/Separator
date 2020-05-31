using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    public class Friend
    {
        private IConsole myConsole;
        private string name;
        public List<Dish> dishes;
       
 
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
            myConsole.WriteLine("Input name of dish");
            string name = myConsole.ReadLine();
            myConsole.WriteLine("Input cost");
            int cost = int.Parse(myConsole.ReadLine());
            dishes.Add(new Dish(name, cost));

        }
        public void Add_dish(Dish dish)
        {
            dishes.Add(dish);
        }
        public void Print_all_dishes() { 
        
            foreach(Dish dish in dishes)
            {
                myConsole.WriteLine(dish.Name);
            }
        }
        public decimal TakeCost()
        {
            decimal cost = 0;
            foreach(Dish d in dishes)
            {
                cost = cost + d.Cost;
            }
            return cost;
        }

        public Friend() : this("Unnamed") { }
        public Friend(string name) : this(name, new MyConsole()) { }
        public Friend(string name, IConsole console)
        {
            Name = name;
            myConsole = console;
            dishes = new List<Dish>();
        }

    }
}
