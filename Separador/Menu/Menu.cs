using LiteDB;
using System;

namespace Separator.Menu
{
    public class Menu
    {
        public void SetCurrent(LiteDatabase db, Group group)
        {
            var cur_group = db.GetCollection<Group>("current_group");
            cur_group.DeleteAll();
            cur_group.EnsureIndex(x => x.Name);
            cur_group.Insert(group);            
        }
        public void CreateGroup()
        {
            Console.WriteLine("Under what name will people remember you?\n");
            string group_name = Console.ReadLine();
            Group group = new Group(group_name);
            Console.WriteLine("What is the name of your brave leader?\n");
            string name = Console.ReadLine();
            group.AddNewFriend(name);
            group.Print_all_member();

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Получаем коллекцию
                var col = db.GetCollection<Group>("group");
                col.EnsureIndex(x => x.Name);
                col.Insert(group);
                // Сохраняем ее как активную группу, удалив преведущую
                SetCurrent(db, group);
            }

        }
        public void FindGroup()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                Page findPage = new Page();
                var col = db.GetCollection<Group>("group");
                var groupAll = col.FindAll();
                foreach(Group g in groupAll)
                {
                    findPage.Add(g.Name, () => SetCurrent(db, g));
                }
                findPage.Display();
            }
        }

        private Page Create_welcome_page()
        {
            Page welcome = new Page();
            welcome.Add("New day, new group", () => CreateGroup());
            welcome.Add("Find my group", () => FindGroup());
            //welcome.Add("Find my group", () => Console.WriteLine("Hmmm.. . My memory's not what it was earlier. I can't remember all pepole(task in progress)\n"));
            return (welcome);
        }


        public Menu()
        {
            Console.WriteLine("Welcome to Separator\n It is console app that can help you split the bill with your friend\n");
            Page welcome = Create_welcome_page();
            welcome.Display();
            Group current;
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("current_group");
                current = col.FindOne(Query.All());
                Console.WriteLine(current.Name);
            }
            Main_page main = new Main_page(current);
            main.Display();
        }
    }
}
