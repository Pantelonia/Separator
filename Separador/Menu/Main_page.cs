using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Separator.Menu
{
    public class Main_page : Page
    {
        private Group group;
        private bool exit = false;
        public Group Group
        {
            set
            {
                group = value;
                if (!group.Friends.Any())
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
                col.EnsureIndex(x => x.Name);
                col.Update(Group);
            }
        }
        private void AddNewFriend()
        {
            Group.AddNewFriend(Input.ReadString("I see we got a new guy\nWhat's your name?"));
            UpdateGroup();
        }
        private void AddNewDish()
        {        
            Console.WriteLine("More food!\nWhat is type of your dish");
            Page add_dish = new Page();
            add_dish.Add("It's dish only for me", () => Group.Create_personal_dish());
            add_dish.Add("it's dish for all", () => Group.Create_communal_dish());
            add_dish.Display();  
        }
        private void DeleteFriend()
        {
            Page deleteFriend = new Page();
            foreach(Friend f in Group.Friends)
            {
                deleteFriend.Add(f.Name, () => group.DeleteFriend(f));
            }
            deleteFriend.Display();
            //Сохраняем изменения в базу данных
            UpdateGroup();
        }
        private void ShaowAllFriend()
        {
            Group.Print_all_member();
        }
        private void TakeBill()
        {
            Console.WriteLine($"Current bill:{ Group.Total_cost}");
        }
        private void GoToMenu()
        {
            exit = true;
        }
        private void Separate()
        {
           foreach(Friend f in Group.Friends)
            {
                decimal cost = f.TakeCost();
                Console.WriteLine($"{f.Name} should pay - {cost}");
            }
            
        }

        public void ShowDish()
        {
            Group.ShowAllDish();
        }

        public int Start()
        {
            while (!exit)
            {
                Display();
            }
            return 0;
        }

        private void Init()
        {
            Add("Add new friend", () => AddNewFriend());
            Add("Add new dish", () => AddNewDish());
            Add("Show all friend's dish", () => ShowDish());
            Add("Banish a friend", () => DeleteFriend());
            Add("Show me my friend", () => ShaowAllFriend());
            Add("Give me the bill", () => TakeBill());
            Add("Separete!", () => Separate());
            Add("Back to welcome page", () => GoToMenu());
        }

        
        public Main_page(Group group)
        {
            Group = group;
            Init();
        }
    }
}
