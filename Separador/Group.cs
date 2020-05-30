using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Separator
{
    public class Group
    {
        public Guid Id { get; set; }
        private string name;
        private decimal total_cost;
        public List<Friend> Friends { get; set; }
        private IConsole myConsole;
      

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
        public decimal Total_cost
        {
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be greater than zero");
                total_cost = value;
            }
            get
            {
                return total_cost;
            }
        }

        public void AddNewFriend(string name)
        {
            Friend friend = new Friend(name);
            Friends.Add(friend);
            myConsole.WriteLine($"Welcome to the {Name} group!\n" );
        }
        public void DeleteFriend(Friend friend)
        {
            Friends.Remove(friend);
            myConsole.WriteLine($"Goodbye, dear friend!\nMembers {friend.Name} of leave from {Name} group:\n");
        }
        private Dish ComposeDish(bool type)
        {

            myConsole.WriteLine("Input name of dish");
            string name = myConsole.ReadLine();
            myConsole.WriteLine("Input cost");
            int cost = int.Parse(myConsole.ReadLine());
            Total_cost = Total_cost + cost;
            if (type)
                cost = cost / Friends.Count();
            return new Dish(name, cost, type);
            
        }
        public void Create_personal_dish()
        {
            myConsole.WriteLine("Choose friend:\n");
            Menu.Page personalDish = new Menu.Page(myConsole);
            foreach (Friend f in Friends)
            {
                personalDish.Add(f.Name, () => f.Add_dish(ComposeDish(false)));
            }
            personalDish.Display();
        }
        public void Create_communal_dish()
        {
            Dish dish = ComposeDish(true);
            foreach (Friend friend in Friends)
            {
                friend.Add_dish(dish);
            }
        }

        public void Print_all_member()
        {
            myConsole.WriteLine($"Members of {Name} group:");
            foreach (Friend friend in Friends)
            {
                myConsole.WriteLine($"Name of friend:{friend.Name}");
            }
        }

        public Group() : this("Unnamed") { }
        public Group(string name) : this(name, new MyConsole()) { }
        public Group(string name, IConsole console) : this(name, console, 0) { }
        public Group(string name, IConsole console, decimal total_cost)
        {
            Name = name;
            Total_cost = total_cost;
            Friends = new List<Friend>();
            myConsole = console;
        }
    }
}
