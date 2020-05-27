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
            Console.WriteLine("Welcome to the {0} group!\n", Name);
        }
        public void DeleteFriend(Friend friend)
        {
            Friends.Remove(friend);
            Console.WriteLine("Goodbye, dear friend!\nMembers {0} of leave from {1} group:\n", friend.Name, Name);
        }
        private Dish ComposeDish(bool type)
        {

            Console.WriteLine("Input name of dish");
            string name = Console.ReadLine();
            Console.WriteLine("Input cost");
            int cost = int.Parse(Console.ReadLine());
            Total_cost = Total_cost + cost;
            if (type)
                cost = cost / Friends.Count();
            return new Dish(name, cost, type);
            
        }
        public void Create_personal_dish()
        {
            Console.WriteLine("Choose friend:\n");
            Menu.Page personalDish = new Menu.Page();
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
            Console.WriteLine("Members of {0} group:", Name);
            foreach (Friend friend in Friends)
            {
                Console.WriteLine($"Name of friend:{friend.Name}");
            }
        }

        public Group() : this("Unnamed") { }
        public Group(string name) : this(name, 0) { }
        public Group(string name, decimal total_cost)
        {
            Name = name;
            Total_cost = total_cost;
            Friends = new List<Friend>();
        }
    }
}
