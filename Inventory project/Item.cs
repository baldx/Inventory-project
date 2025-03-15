using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class Item
    {
        public int Id;
        public string Category;
        public string Name;
        public int MaxStack;
        public int CurrentStack;

        public Item(int id, string category, string name, int maxStack, int currentStack) //default propities of class
        {
            this.Id = id;
            this.Category = category;
            this.Name = name;
            this.MaxStack = maxStack;
            CurrentStack = currentStack;
        }

        public virtual void Display() //logs the item, availave for override
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Category: {Category}, Max stack: {MaxStack}");
        }

        public virtual void Use()
        {
            if (CurrentStack > 0 )
            {
                CurrentStack--;
                Console.WriteLine($"Used one {Name}. Remaining {CurrentStack}");

                if (CurrentStack == 0)
                {
                    Console.WriteLine($"{Name} stack is empty!");
                }
            }
        }

    }
}
