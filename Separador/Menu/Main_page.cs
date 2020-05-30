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
        private IConsole myConsole;
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
            myConsole.WriteLine("I see we got a new guy\nWhat's your name?");
            Group.AddNewFriend(myConsole.ReadLine());
            UpdateGroup();
            Display();
        }
        private void AddNewDish()
        {
            try
            {
                myConsole.WriteLine("More food!\nWhat is type of your dish");
                Page add_dish = new Page(myConsole);
                add_dish.Add("It's dish only for me", () => Group.Create_personal_dish());
                add_dish.Add("it's dish for all", () => Group.Create_communal_dish());
                add_dish.Display();
                Display();
            }
            catch(ArgumentOutOfRangeException ex)
            {
                myConsole.WriteLine($"Исключение: {ex.ParamName}");

            }

        }
        private void DeleteFriend()
        {
            Page deleteFriend = new Page(myConsole);
            foreach(Friend f in Group.Friends)
            {
                deleteFriend.Add(f.Name, () => group.DeleteFriend(f));
            }
            deleteFriend.Display();
            //Сохраняем изменения в базу данных
            UpdateGroup();
            Display();

        }
        private void ShaowAllFriend()
        {
            Group.Print_all_member();
            Display();
        }
        private void TakeBill()
        {
            myConsole.WriteLine($"Current bill:{ Group.Total_cost}");
            Display();
        }
        private void GoToMenu()
        {
            Menu menu = new Menu();
        }
        private void Separate()
        {
           foreach(Friend f in Group.Friends)
            {
                decimal cost = f.TakeCost();
                myConsole.WriteLine($"{f.Name} should pay - {cost}");
            }
            Display();
        }

        public Main_page(Group group, IConsole console)
        {
            Group = group;
            myConsole = console;
            Add("Add new friend", () => AddNewFriend());
            Add("Add new dish", () => AddNewDish());
            Add("Banish a friend", () => DeleteFriend());
            Add("Show me my friend", () => ShaowAllFriend());
            Add("Give me the bill", () => TakeBill());
            Add("Separete!", () => Separate());
            Add("Back to welcome page", () => GoToMenu());
        }
        public Main_page(Group group)
        {
            Group = group;
            myConsole = new MyConsole();
            Add("Add new friend", () => AddNewFriend());
            Add("Add new dish", () => AddNewDish());
            Add("Banish a friend", () => DeleteFriend());
            Add("Show me my friend", () => ShaowAllFriend());
            Add("Give me the bill", () => TakeBill());
            Add("Separete!", () => Separate());
            Add("Back to welcome page", () => GoToMenu());
        }
    }
}
