using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class MyProgram
    {
        internal void Run()
        {
            InventoryClass inventory = new InventoryClass();

            for (int i = 0; i < 256; i++)
            {
                inventory.PickUpItem(new Block(0, "Dirt", 5, 1));
            }


            inventory.Display();
            inventory.ShowTotalSlots();

            Tool pickaxe = new Tool(69, "Wood Pickaxe", 64, 1);


            inventory.Inventory[0, 0] = pickaxe.Name;

            //inventory.PickUpItem(pickaxe);
            //inventory.Display();
            //inventory.ChangeItemPositionTo(0, 0, 2, 8);
            //inventory.SortInventoryByName();
            //inventory.Display();


            //Consumable NightVision = new Consumable(1, "Potion of Night vision");
            //inventory.PickUpItem(NightVision);
            //inventory.SortInventoryByName();
            //inventory.Display();

        }
    }
}
