using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class Consumable : Item
    {
        public Consumable (int id, string name, int currentStack, string category = "Consumable", int maxStack = 16 ) 
            :base(id, category, name, maxStack, currentStack)
        {

        }

        public override void Use()
        {
            Console.WriteLine($"{Name} has been consumed");
        }
    }
}
