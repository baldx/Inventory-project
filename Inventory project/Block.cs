using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class Block : Item
    {
        public int BlockResistance;

        public Block(int id, string name, int blockResistance, int currentStack, string category = "Block", int maxStack = 64)
            : base(id, category, name, maxStack, currentStack)
        {
            this.BlockResistance = blockResistance;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} has been placed");
        }
    }
}
