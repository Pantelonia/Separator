using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    public class Friend
    {
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
            string name = Input.ReadString("Input name of dish");
            int cost = Input.ReadInt("Input cost", min: 0, max: Int32.MaxValue);
            dishes.Add(new Dish(name, cost));

        }
        public void Add_dish(Dish dish)
        {
            dishes.Add(dish);
        }
        public void Print_all_dishes() { 
        
            foreach(Dish dish in dishes)
            {
                Console.WriteLine(dish.Name);
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
        public Friend(string name)
        {
            Name = name;
            dishes = new List<Dish>();
        }

    }
}
