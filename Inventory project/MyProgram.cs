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
            InventoryClass inventory = new InventoryClass(); //define an inventory
            Tool pickaxe = new Tool(69, "Wood Pickaxe", 64, 1); //creates a new tool
            Block diamondBlock = new Block(23, "Diamond Block", 20, 1); //creates a new block

            bool isRunning = true;
            string[] options =
            {
                "1. Use item",
                "2. Pick up item",
                "3. Drop item"
            }; //options for user

            for (int i = 0; i < 256; i++) //for loop to add a dirt block
            {
                inventory.PickUpItem(new Block(0, "Dirt block", 5, 1));
            }


            inventory.PickUpItem(pickaxe);
            inventory.PickUpItem(diamondBlock);


            while (isRunning) //logic to run the program
            {
                StartingUI();
            }

            void StartingUI() //User interface for the program
            {
                inventory.Display(); //display inventory

                foreach (string option in options) //display options from the array
                {
                    Console.WriteLine(option);
                }

                string input = Convert.ToString(Console.ReadLine()); //get user input

                if (input == "1") //if user chose option 1
                {
                    Console.Clear(); //clear console
                    Console.WriteLine("Choose the item you want to use!");
                    int index = 1; //defines an index

                    foreach (Item item in inventory.Inventory) //for each item in inventory
                    {
                        if (item is Item) //if item is item, not an empty array
                        {
                            Console.WriteLine($"{index}. {item.CurrentStack} {item.Name}"); //display index, current stack and name of item
                            index++; //increment index
                        }
                    }

                    Console.ReadLine();
                }
                
            }



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
