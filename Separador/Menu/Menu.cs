using LiteDB;
using System;
using System.IO;


namespace Separator.Menu
{
    public class Menu
    {
        private bool exit = false;

        public void SetCurrent(LiteDatabase db, Group group)
        {
            var cur_group = db.GetCollection<Group>("current_group");
            cur_group.DeleteAll();
            cur_group.Insert(group);            
        }

        public void CreateGroup()
        {
            
            string group_name = Input.ReadString("Under what name will people remember you?\n");
            Group group = new Group(group_name);
            string name = Input.ReadString("What is the name of your brave leader?\n");
            group.AddNewFriend(name);
            group.Print_all_member();

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Получаем коллекцию
                var col = db.GetCollection<Group>("group");
                col.Insert(group);
                // Сохраняем ее как активную группу, удалив преведущую
                SetCurrent(db, group);
            }

        }

        public void FindGroup()
        {
            Group check;
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("group");
                check = col.FindOne(Query.All());
            }
            if (check == null)
            {
                Console.WriteLine("Hmmm.. . My memory's not what it was earlier. I can't remember some group\n(Create some group befor finding)\n");
                CreateGroup();
            }
            else
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    Page findPage = new Page();
                    var col = db.GetCollection<Group>("group");
                    var groupAll = col.FindAll();

                    foreach (Group g in groupAll)
                    {
                        findPage.Add(g.Name, () => SetCurrent(db, g));
                    }
                    findPage.Display();
                }
            }
           
        }

        public void DeleteAll()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("group");
                col.DeleteAll();

            }
            StartMenu();
        }

        private void Delete(Group g, LiteDatabase db)
        {
            using (db)
            {
                var col = db.GetCollection<Group>("group");
                col.Delete(g.Id);
            }
            

        }
        private void DeleteGroup()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                Page deletePage = new Page();
                var col = db.GetCollection<Group>("group");
                var groupAll = col.FindAll();
                foreach (Group g in groupAll)
                {
                    deletePage.Add(g.Name, () => Delete(g, db) );
                }
                deletePage.Display();                
            }
        }

        private Page Create_welcome_Page()
        {
            Page welcome = new Page();
            welcome.Add("New day, new group", () => CreateGroup());
            welcome.Add("Find my group", () => FindGroup());
            welcome.Add("Delete group", () => DeleteGroup());
            welcome.Add("Delete all gruops", () => DeleteAll());
            welcome.Add("Exit", () => exit  = true);
            return (welcome);
        }

        public int StartMenu()
        {
            while (!exit)
            {
                Console.WriteLine("Welcome to Separator\n It is myConsole app that can help you split the bill with your friend\n");
                Page welcome = Create_welcome_Page();
                welcome.Display();
                if (!exit)
                {
                    Group current;
                    using (var db = new LiteDatabase(@"MyData.db"))
                    {
                        var col = db.GetCollection<Group>("current_group");
                        current = col.FindOne(Query.All());
                    }
                    Main_page main = new Main_page(current);
                    main.Start();
                }
               
            }
            return 0;
            
        }

    
    }
}
