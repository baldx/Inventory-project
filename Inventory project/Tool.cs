using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class Tool : Item
    {
        public int Durability; //define the tools durability

        public Tool(int id, string name, int durability, int currentStack = 1, string category = "Tool", int maxStack = 1) //define tool
            :base (id, category, name, maxStack, currentStack)
        {
            this.Durability = durability;
        }

        public override void Display() //logs the tool
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Category: {Category}, Max stack: {MaxStack}, Durability: {Durability}");
        }
    }
}
