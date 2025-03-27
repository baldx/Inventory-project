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
                "3. Drop item",
                "4. Sort inventory"
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

                    List<Item> selectableItems = new(); //creates a list to map user input to inventory items

                    foreach (Item slot in inventory.Inventory) //for each item in inventory
                    {
                        if (slot != null) //if item is item, not an empty array
                        {
                            
                            Console.WriteLine($"{index}. {slot.CurrentStack} {slot.Name}"); //display index, current stack and name of item
                            selectableItems.Add(slot);
                            index++; //increment index
                        }

                    }

                    string useItemInput = Console.ReadLine();


                    if (int.TryParse(useItemInput, out int selectedIndex) &&
                        selectedIndex > 0 && selectedIndex <= selectableItems.Count)
                    {
                        Item selectedItem = selectableItems[selectedIndex - 1];

                        // Logging item usage (Example: Print a message)

                        Console.Clear();

                        if (selectedItem is Block block)
                        {
                            Console.WriteLine("How many blocks do u want to place?");
                            string userInput = Console.ReadLine(); //get user input


                            if (int.TryParse(userInput, out int amount)) //returns bool after parsing user input
                            {
                                if (block.CurrentStack >= amount) // compare the parsed integer with CurrentStack
                                {
                                    block.CurrentStack -= amount; // subtract amount from CurrentStack
                                    Console.WriteLine($"You used {amount} {block.Name}. Remaining: {block.CurrentStack}");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough blocks available.");
                                }
                            }
                        }
                        else if (selectedItem is Tool tool)
                        {
                            Console.WriteLine("How many times to do u want to use this tool:");
                            Console.WriteLine($"Current durability: {tool.Durability}");
                            string userInput = Console.ReadLine(); //get user input


                            if (int.TryParse(userInput, out int amount)) //returns bool after parsing user input
                            {
                                if (tool.Durability >= amount) // compare the parsed integer with CurrentStack
                                {
                                    tool.Durability -= amount; // subtract amount from CurrentStack
                                    Console.WriteLine($"You swung the {tool.Name}!, current durability: {tool.Durability}");
                                }
                                else
                                {
                                    Console.WriteLine("Cant use the pickaxe that many times!");
                                }
                            }
                        }
                        else if (selectedItem is Consumable consumable)
                        {
                            Console.WriteLine($"Consumed {consumable.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"You used {selectedItem.Name}!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Try again.");
                    }


                    Console.ReadLine();
                }
                else if (input == "4")
                {
                    Console.Clear();
                    inventory.SortInventoryByName();
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
