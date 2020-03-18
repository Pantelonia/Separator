using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    class Group
    {
        private string name;
        private decimal total_cost;
        private List<Friend> friends;

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

        public void Add_friend(Friend friend)
        {
            friends.Add(friend);
        }

        public void Print_all_member()
        {
            foreach (Friend friend in friends)
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
            friends = new List<Friend>();
        }
    }
}
