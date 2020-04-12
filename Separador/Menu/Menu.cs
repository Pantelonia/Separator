using System;

namespace Separator.Menu
{
    public class Menu
    {
        public void Create_group()
        {
            Console.WriteLine("Under what name will people remember you?\n");
            string group_name = Console.ReadLine();
            Group group = new Group(group_name);
            Console.WriteLine("What is the name of your brave leader?\n");
            string name = Console.ReadLine();
            group.Add_friend(name);
            group.Print_all_member();

        }

        private Page Create_welcome_page()
        {
            Page welcome = new Page();
            welcome.Add("New day, new group", () => Create_group());
            welcome.Add("Find my group", () => Console.WriteLine("Hmmm.. . My memory's not what it was earlier. I can't remember all pepole(task in progress)\n"));
            return (welcome);
        }


        public Menu()
        {
            Console.WriteLine("Welcome to Separator\n It is console app that can help you split the bill with your friend\n");
            Page welcome = Create_welcome_page();
            welcome.Display();
            Console.ReadKey();       





        }
    }
}
