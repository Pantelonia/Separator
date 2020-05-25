using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Separator.Menu
{
    class Main_page : Page
    {
        private Group group; 
        public Group Group
        {
            set
            {
                group = value;
                if (!group.friends.Any())
                {
                   group.AddNewFriend("Unnamed");
                }
            }
            get
            {
               return group;
            }
        }
        private void UpdateGroup()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("group");
                var group = col.Find(x => x.Name.Equals(Group.Name));
                col.Update(group);
            }
        }
        private void AddNewFriend()
        {
            Console.WriteLine("I see we got a new guy\nWhat's your name?");
            Group.AddNewFriend(Console.ReadLine());
            UpdateGroup();
        }
        private void Create_personal_dish()
        {
            Console.WriteLine("Choose friend:\n");
            Group.Print_all_member();
            string input = Console.ReadLine();
            int choice;

            while (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter an integer");
                input = Console.ReadLine();
            }
            group.friends[choice - 1].Add_dish();
        }
        private void Create_communal_dish()
        {
            Console.WriteLine("Input name of dish");
            string name = Console.ReadLine();
            Console.WriteLine("Input cost");
            int cost = int.Parse(Console.ReadLine());
            int dif_cost = cost / group.friends.Count();
            foreach(Friend friend in group.friends)
            {
                friend.Add_dish(new Dish(name, dif_cost, true));
            }                   
        }
        private void AddNewDish()
        {         
            Console.WriteLine("More food!\nWhat is type of your dish");
            Page add_dish = new Page();
            add_dish.Add("It's dish only for me", () => Create_personal_dish());
            add_dish.Add("it's dish for all", () => Create_communal_dish());
        }
        private void DeleteFriend()
        {
            Page deleteFriend = new Page();
            foreach(Friend f in Group.friends)
            {
                deleteFriend.Add(f.Name, () => group.DeleteFriend(f));
            }
            deleteFriend.Display();
            //Сохраняем изменения в базу данных
            UpdateGroup();
        }
        public Main_page(Group group)
        {
            Group = group;
            Add("Add new friend", () => AddNewFriend());
            Add("Add new dish", () => AddNewDish());
            Add("Banish a friend", () => DeleteFriend());

        }
    }
}
