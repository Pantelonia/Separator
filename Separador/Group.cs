﻿using System;
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
            Console.WriteLine($"Welcome to the {Name} group!\n" );
        }
        public void DeleteFriend(Friend friend)
        {
            Friends.Remove(friend);
            Console.WriteLine($"Goodbye, dear friend!\nMembers {friend.Name} of leave from {Name} group:\n");
        }
        private Dish ComposeDish(bool type)
        {
            string name = Input.ReadString("Input name of dish");
            int cost = Input.ReadInt("Input cost", min:0,max: Int32.MaxValue);
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
            Console.WriteLine($"Members of {Name} group:");
            foreach (Friend friend in Friends)
            {
                Console.WriteLine($"Name of friend:{friend.Name}");
            }
        }
        public void ShowAllDish()
        {
            foreach(Friend f in Friends)
            {
                Console.WriteLine($"\n{f.Name}'s dishes:\n");
                foreach (Dish dish in f.dishes)
                    Console.WriteLine($"Name:{dish.Name} Cost:{dish.Cost}");
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
