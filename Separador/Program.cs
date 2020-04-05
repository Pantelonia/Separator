using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Separator
{
    class Program
    {
        static void Main(string[] args)
        {
            Dish dish = new Dish( "Eggs", 40, true);
            Group group = new Group("Pokorili");
            Friend paul = new Friend("Paul");
            group.Add_friend(paul);

            Friend serjey = new Friend("Serjey");
            group.Add_friend(serjey);

            Friend alex = new Friend("Alex");
            group.Add_friend(alex);

            Friend vova = new Friend("Vova");
            group.Add_friend(vova);

            paul.Add_dish(dish);
            paul.Print_all_dishes();

            group.Print_all_member();
            Console.WriteLine(dish.Name);
            Console.ReadKey();

        }
    }
}
