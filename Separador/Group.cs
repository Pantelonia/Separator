using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    public class Group
    {
        private string name;
        private decimal total_cost;
        public List<Friend> friends;
      

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
            friends.Add(friend);
            Console.WriteLine("Welcome to the club!\nMembers of {0} group:\n", Name);
            Print_all_member();
        }
        public void DeleteFriend(Friend friend)
        {
            friends.Remove(friend);
            Console.WriteLine("Goodbye, dear friend!\nMembers of {0} group:\n", Name);
            Print_all_member();
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
