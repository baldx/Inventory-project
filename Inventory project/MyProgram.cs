using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class MyProgram
    {
        internal void Run()
        {
            InventoryClass inventory = new InventoryClass(); //define an inventory
            Tool pickaxe = new Tool(69, "Wood Pickaxe", 64); //creates a new tool
            Block diamondBlock = new Block(23, "Diamond Block", 20, 1); //creates a new block

            bool isRunning = true;
            string[] options =
            {
                "1. Use item",
                "2. Pick up item",
                "3. Drop item",
                "4. Sort inventory",
                "5. Exit inventory"
            }; //options for user

            object[] itemsForPickingUp =
            {
                //  items
                new Tool(68, "Diamond Sword", 1256),
                new Consumable(101, "Strength Potion", 1),
                new Consumable(102, "Carrot Cake", 1),
                new Tool(67, "Hyperion", 9999),
                new Tool(66, "Dark Claymore", 700),
                new Block(22, "Bedrock", 9999, 44),

                //  tools
                new Tool(69, "Emerald Pickaxe", 1500),
                new Tool(70, "Dragon Axe", 2300),
                new Tool(71, "Magic Wand", 5000),

                //  consumables
                new Consumable(103, "Mana Potion", 1),
                new Consumable(104, "Golden Apple", 3),
                new Consumable(105, "Elixir of Speed", 5),

                //  blocks
                new Block(23, "Obsidian", 1000, 55),
                new Block(24, "Gold Block", 200, 62),
                new Block(25, "Diamond Block", 500, 12)
            };

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
                    bool isTrue = true;
                    while (isTrue)
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
                        Console.WriteLine("Enter X to exit");

                        string useItemInput = Console.ReadLine();

                        if (useItemInput == "X" || useItemInput == "x") //logic to exit the action
                        {
                            isTrue = false;
                            return;
                        }

                        if (int.TryParse(useItemInput, out int selectedIndex) &&
                            selectedIndex > 0 && selectedIndex <= selectableItems.Count)
                        {
                            Item selectedItem = selectableItems[selectedIndex - 1]; //selected item is equals to the selected item from the array

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
                    
                }
                else if (input == "2")
                {
                    Console.Clear();

                    List<Item> selectableItems; //creates a list to map user input to inventory items
                    int indexList;


                    bool isTrue = true;

                    while (isTrue)
                    {

                        indexList = 1;
                        selectableItems = new();

                        foreach (Item item in itemsForPickingUp) //for each item in inventory
                        {
                            if (item != null) //if item is item, not an empty array
                            {

                                Console.WriteLine($"{indexList}. {item.CurrentStack} {item.Name}"); //display index, current stack and name of item
                                selectableItems.Add(item);
                                indexList++; //increment index
                            }

                        }
                        Console.WriteLine("Enter X to go back");

                        string useItemInput = Console.ReadLine();

                        if (useItemInput == "x" || useItemInput == "X") //logic to exit program
                        {
                            isTrue = false;
                        }

                        if (int.TryParse(useItemInput, out int selectedIndex) &&
                        selectedIndex > 0 && selectedIndex <= selectableItems.Count)
                        {
                            Item selectedItem = selectableItems[selectedIndex - 1]; //selects the item from the list


                            Console.Clear();

                            if (selectedItem is Block block)
                            {
                                bool isTruee = true; //add variable for loop to true
                                while (isTruee)//while true
                                {
                                    Console.Clear();
                                    Console.WriteLine("How many blocks do u want to pick up?");
                                    Console.WriteLine($"Current stack {block.CurrentStack}/{block.MaxStack}");
                                    string userInput = Console.ReadLine(); //get user input


                                    if (int.TryParse(userInput, out int amount)) //returns bool after parsing user input
                                    {
                                        if (block.CurrentStack >= amount) // compare the parsed integer with CurrentStack
                                        {
                                            block.CurrentStack -= amount;  // subtract amount from CurrentStack
                                            Block pickedBlock = new Block(block.Id, block.Name, block.BlockResistance, amount);//create new block with the amount picked up
                                            Console.WriteLine($"You picked up {amount} {block.Name}. Remaining: {block.CurrentStack}"); //display item picked up

                                            if (block.CurrentStack == 0) //if blocks are 0
                                            {
                                                itemsForPickingUp = itemsForPickingUp.Where(item => item != block).ToArray(); // remove the block from the list
                                                isTruee = false;
                                            }

                                            inventory.PickUpItem(pickedBlock); //pickup item
                                            isTruee = false; //sets loop to false
                                        }
                                        else //display error 
                                        {
                                            Console.WriteLine("Not enough blocks available. Press any key to try again");
                                            Console.WriteLine($"Current stack {block.CurrentStack}/{block.MaxStack}");
                                            Console.ReadLine();
                                        }
                                    }
                                }
                            }
                            
                            else if (selectedItem is Tool tool)
                            {
                                Tool pickedTool = new Tool(tool.Id, tool.Name, tool.Durability);//create new tool with the selectoed tool
                                itemsForPickingUp = itemsForPickingUp.Where(item => item != tool).ToArray(); // remove the tool from the list
                                inventory.PickUpItem(pickedTool); //add tool to inventory
                                Console.WriteLine($"{pickedTool.Name} Has been picked up. Press any key to return to inventory");
                            }
                            
                            else if (selectedItem is Consumable consumable)
                            {
                                bool isTruee = true; //add variable for loop to true
                                while (isTruee) //while true
                                {
                                    Console.Clear();
                                    Console.WriteLine("How many consumables do you want to pick up?");
                                    Console.WriteLine($"Current stack {consumable.CurrentStack}/{consumable.MaxStack}");
                                    string userInput = Console.ReadLine(); //get user input

                                    if (int.TryParse(userInput, out int amount)) //returns bool after parsing user input
                                    {
                                        if (consumable.CurrentStack >= amount) // compare the parsed integer with CurrentStack
                                        {
                                            consumable.CurrentStack -= amount;  // subtract amount from CurrentStack
                                            Consumable pickedConsumable = new Consumable(consumable.Id, consumable.Name, amount); //create new consumable with the amount picked up
                                            Console.WriteLine($"You picked up {amount} {consumable.Name}. Remaining: {consumable.CurrentStack}"); //display item picked up

                                            if (consumable.CurrentStack == 0) //if no consumables left
                                            {
                                                itemsForPickingUp = itemsForPickingUp.Where(item => item != consumable).ToArray(); // remove the consumable from the list
                                                isTruee = false;
                                            }

                                            inventory.PickUpItem(pickedConsumable); //pickup item
                                            isTruee = false; //sets loop to false
                                        }
                                        else //display error 
                                        {
                                            Console.WriteLine("Not enough consumables available. Press any key to try again");
                                            Console.WriteLine($"Current stack {consumable.CurrentStack}/{consumable.MaxStack}");
                                            Console.ReadLine();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong. Try again");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Try again.");
                        }

                        Console.ReadLine();
                        Console.Clear();

                    }
                    
                }

                else if (input == "3")
                {
                    bool isTrue = true;

                    while (isTrue) //while loop to not restard program
                    {
                        Console.Clear();
                        Console.WriteLine("Choose the item you want to drop:");
                        int index = 1;

                        List<Item> selectableItems = new(); //creates new list

                        foreach (Item slot in inventory.Inventory) //displays all items in inventory
                        {
                            if (slot != null)
                            {
                                Console.WriteLine($"{index}. {slot.CurrentStack} {slot.Name}");
                                selectableItems.Add(slot);
                                index++;
                            }
                        }

                        if (selectableItems.Count == 0) //if inventory is empty
                        {
                            Console.WriteLine("Inventory is empty. Press any key to return.");
                            Console.ReadLine();
                            return;
                        }

                        Console.WriteLine("Enter X to return");
                        string dropInput = Console.ReadLine();

                        if (dropInput == "x" || dropInput == "X") //to exit and return to start
                        {
                            isTrue = false;
                            return;
                        }

                        if (int.TryParse(dropInput, out int selectedIndex) &&
                            selectedIndex > 0 && selectedIndex <= selectableItems.Count) //logic to select user input
                        {
                            Item selectedItem = selectableItems[selectedIndex - 1]; //item selected is 1 less of the selected index in array

                            Console.Clear();

                            if (selectedItem is Block block) //if selected item is block
                            {
                                Console.WriteLine("How many blocks do you want to drop?");
                                Console.WriteLine($"Current stack: {block.CurrentStack}"); //display some text
                                string amountInput = Console.ReadLine();

                                if (int.TryParse(amountInput, out int amount) && amount > 0) //if amount is bigger than 0
                                {
                                    if (block.CurrentStack >= amount)
                                    {
                                        block.CurrentStack -= amount; //removes total amount
                                        Console.WriteLine($"Dropped {amount} {block.Name}. Remaining: {block.CurrentStack}");

                                        if (block.CurrentStack == 0) //if stack is 0
                                        {
                                            inventory.DropItem(block); //deletes item
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough to drop that amount.");
                                    }
                                }
                            }
                            else if (selectedItem is Tool tool)
                            {
                                Console.WriteLine($"Dropped {tool.Name}.");
                                inventory.DropItem(tool); //delete item
                            }
                            else if (selectedItem is Consumable consumable)
                            {
                                Console.WriteLine("How many do you want to drop?");
                                Console.WriteLine($"Current stack: {consumable.CurrentStack}"); //display text
                                string amountInput = Console.ReadLine();

                                if (int.TryParse(amountInput, out int amount) && amount > 0) //if amount is more than 0
                                {
                                    if (consumable.CurrentStack >= amount) //currentstack is more than amount
                                    {
                                        consumable.CurrentStack -= amount;
                                        Console.WriteLine($"Dropped {amount} {consumable.Name}. Remaining: {consumable.CurrentStack}");

                                        if (consumable.CurrentStack == 0)//if stack is 0
                                        {
                                            inventory.DropItem(consumable); //delete item
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough to drop that amount.");
                                    }
                                }
                            }

                            Console.WriteLine("Press any key to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Try again.");
                            Console.ReadLine();
                        }
                    }
                }
                else if (input == "4")
                {
                    Console.Clear();
                    inventory.SortInventoryByName();
                }
                else if (input == "5")
                {
                    Environment.Exit(0);
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
