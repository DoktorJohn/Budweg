using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Budweg
{
    public class Menu
    {
        private string Title;
        public MenuItem[] MenuItems = new MenuItem[10];
        private int ItemCount = 0;

        //Titel på menu
        public Menu(string Title)
        {
            this.Title = Title;
        }

        //Tílføj nyt valg til menuen
        public void AddMenuItem(string menuTitle)
        {
            MenuItem mi = new MenuItem(menuTitle);
            MenuItems[ItemCount] = mi;
            ItemCount++;

        }

        
        public void Show()
        {
            Console.WriteLine(this.Title);
            Console.WriteLine("Menuvalg:");

            for (int i = 0; i < ItemCount; i++)
            {
                Console.WriteLine(MenuItems[i].Title);

            }

          
        }


        public int selectMenuItem()
        {
            string selection = Console.ReadLine();
            if (int.TryParse(selection, out int select))
            {
                return select;
            }

            else
            {
                Console.WriteLine("Angiv venligst et nummer.");
            }

            return select;
        }


    }

}
