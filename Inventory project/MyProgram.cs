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
            string[] menuOptions = { //options for menu
            "1. Use item",
            "2. Pick up item",
            "3. Drop item",
            "4. Sort inventory",
            "5. Exit inventory"
        };

            object[] itemsForPickingUp = {
            //tools
            new Tool(68, "Diamond Sword", 1256),
            new Tool(67, "Hyperion", 9999),
            new Tool(66, "Dark Claymore", 700),
            new Tool(69, "Emerald Pickaxe", 1500),
            new Tool(70, "Dragon Axe", 2300),
            new Tool(71, "Magic Wand", 5000),
            //consumables
            new Consumable(101, "Strength Potion", 1),
            new Consumable(102, "Carrot Cake", 1),
            new Consumable(103, "Mana Potion", 1),
            new Consumable(104, "Golden Apple", 3),
            new Consumable(105, "Elixir of Speed", 5),
            //blocks
            new Block(22, "Bedrock", 9999, 44),
            new Block(23, "Obsidian", 1000, 55),
            new Block(24, "Gold Block", 200, 62),
            new Block(25, "Diamond Block", 500, 12)
        };

            //initialize inventory and starting items
            InventoryClass inventory = new InventoryClass();
            Tool pickaxe = new Tool(69, "Wood Pickaxe", 64);
            Block diamondBlock = new Block(23, "Diamond Block", 20, 1);

            //add 256 dirt blocks to inventory
            for (int i = 0; i < 256; i++)
            {
                inventory.PickUpItem(new Block(0, "Dirt block", 5, 1));
            }

            //add items
            inventory.PickUpItem(pickaxe);
            inventory.PickUpItem(diamondBlock);

            //mMain program loop
            bool isRunning = true;
            while (isRunning)
            {
                DisplayMenuAndHandleInput(inventory, ref isRunning); //call function and takes inventory and reference for isRunning variable as parameters
            }
        }

        //displays the main menu and processes user input
        void DisplayMenuAndHandleInput(InventoryClass inventory, ref bool isRunning) //takes inventory and reference of bool as parameter
        {
            Console.Clear();
            inventory.Display();
            foreach (string option in menuOptions) //display option selections
            {
                Console.WriteLine(option);
            }

            string input = Console.ReadLine();
            switch (input) //add logic to get user input
            {
                case "1":
                    HandleUseItem(inventory);
                    break;
                case "2":
                    HandlePickUpItem(inventory);
                    break;
                case "3":
                    HandleDropItem(inventory);
                    break;
                case "4":
                    Console.Clear();
                    inventory.SortInventoryByName(); //sorts inventory
                    break;
                case "5":
                    isRunning = false;
                    Environment.Exit(0); //exits program
                    break;
                default: //defualts to this after not getting any specified input
                    Console.WriteLine("Invalid input. Press any key to continue...");
                    Console.ReadLine();
                    break;
            }
        }

        //handles the "use item" menu option
        void HandleUseItem(InventoryClass inventory)
        {
            Item selectedItem = SelectItemFromInventory(inventory, "Choose the item you want to use:");
            if (selectedItem == null) return;

            Console.Clear();
            if (selectedItem is Block block)
            {
                HandleBlockUsage(inventory, block);
            }
            else if (selectedItem is Tool tool)
            {
                Console.WriteLine($"Used {tool.Name}.");
                inventory.DropItem(tool);
                PauseAndContinue();
            }
            else if (selectedItem is Consumable consumable)
            {
                HandleConsumableUsage(inventory, consumable);
            }
        }

        //handles the "Pick up item" menu option
        void HandlePickUpItem(InventoryClass inventory)
        {
            bool isTrue = true;
            while (isTrue)
            {
                Item selectedItem = SelectItemFromList(itemsForPickingUp, "Choose the item you want to pick up:"); //gets an item that is selected
                if (selectedItem == null) //if item doesnt exist return false
                {
                    isTrue = false;
                    continue;
                }

                Console.Clear();
                if (selectedItem is Block block) //if item is block handle block logic
                {
                    HandleBlockPickup(inventory, block);
                }
                else if (selectedItem is Tool tool) //if item is tool, handle tool 
                {
                    Tool pickedTool = new Tool(tool.Id, tool.Name, tool.Durability);
                    itemsForPickingUp = itemsForPickingUp.Where(item => item != tool).ToArray(); //removes item from list
                    inventory.PickUpItem(pickedTool); //picks up item
                    Console.WriteLine($"{pickedTool.Name} Has been picked up. Press any key to return to inventory");
                    PauseAndContinue();
                }
                else if (selectedItem is Consumable consumable) //if item is consumable, handle consumable logic
                {
                    HandleConsumablePickup(inventory, consumable);
                }
            }
        }

        // Handles the "drop item" menu option
        void HandleDropItem(InventoryClass inventory)
        {
            Item selectedItem = SelectItemFromInventory(inventory, "Choose the item you want to drop:"); //selects item
            if (selectedItem == null) return; //return nothing if item doesnt exist

            Console.Clear();
            if (selectedItem is Block block) //if item is block, run HandleStackableItemDrop
            {
                HandleStackableItemDrop(inventory, block, "block");
            }
            else if (selectedItem is Tool tool)//if item is block drop tool with class
            {
                Console.WriteLine($"Dropped {tool.Name}.");
                inventory.DropItem(tool);
                PauseAndContinue();
            }
            else if (selectedItem is Consumable consumable) //if item is block, run HandleStackableItemDrop
            {
                HandleStackableItemDrop(inventory, consumable, "consumable");
            }
        }

        //displays a list of items from inventory or list and returns the selected item
        Item SelectItemFromList(object[] items, string prompt) //takes object with arrays and string as input
        {
            bool isSelecting = true;
            while (isSelecting)
            {
                Console.Clear();
                Console.WriteLine(prompt);
                List<Item> selectableItems = new(); //creates a new list
                int index = 1;

                foreach (Item item in items) //logic to display all items with their respective index and add them to list
                {
                    if (item != null) //if item exists
                    {
                        Console.WriteLine($"{index}. {item.CurrentStack} {item.Name}");
                        selectableItems.Add(item); //adds item to the list
                        index++;
                    }
                }

                if (selectableItems.Count == 0) //if items count is 0 then display error message
                {
                    Console.WriteLine("No items available. Press any key to return.");
                    Console.ReadLine();
                    return null;
                }

                Console.WriteLine("Enter X to return");
                string input = Console.ReadLine();

                if (!CheckGoBackInput(input)) //logic to check if user wants to return to previous page
                    return null;

                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= selectableItems.Count) //logic to check if item is in bounds and if count is not under 0
                {
                    return selectableItems[selectedIndex - 1]; //gets the current selected index of the list
                }

                Console.WriteLine("Invalid input. Press any key to try again.");
                Console.ReadLine();
            }
            return null;
        }

        //displays inventory items and returns the selected item
        Item SelectItemFromInventory(InventoryClass inventory, string prompt)
        {
            return SelectItemFromList(inventory.Inventory, prompt);
        }

        //gets a valid amount input from the user for stackable items
        int? GetAmountInput(int currentStack, int maxStack, string itemType, string action)
        {
            bool isSelecting = true;
            while (isSelecting) //when method is specified, invokes a while loop
            {
                Console.Clear();
                Console.WriteLine($"How many {itemType}s do you want to {action}?");
                Console.WriteLine($"Current stack: {currentStack}/{maxStack}");
                Console.WriteLine("Enter X to return"); //some UI

                string input = Console.ReadLine(); //get user input

                if (!CheckGoBackInput(input)) return null; //checks if user wants to go back

                if (int.TryParse(input, out int amount) && amount > 0) //returns amount and exists loop if its specified and more than 0
                {
                    return amount;
                }

                Console.WriteLine("Invalid input. Press any key to try again.");
                Console.ReadLine();
            }
            return null; //returns null
        }

        //handles block usage (placing blocks)
        void HandleBlockUsage(InventoryClass inventory, Block block)
        {
            int? amount = GetAmountInput(block.CurrentStack, block.MaxStack, "block", "place"); //gets amount 
            if (amount == null) return; //retuns nothing if amount is not specified

            if (block.CurrentStack >= amount) //if stack is more than amount
            {
                block.CurrentStack -= amount.Value; //remove the amount from stack
                Console.WriteLine($"Placed {amount} {block.Name}. Remaining: {block.CurrentStack}");
                if (block.CurrentStack == 0) //if stack is 0
                {
                    inventory.DropItem(block); //delete item
                }
            }
            else
            {
                Console.WriteLine("Not enough to place that amount.");
            }
            PauseAndContinue();
        }

        //handles consumable usage (consuming items)
        void HandleConsumableUsage(InventoryClass inventory, Consumable consumable)
        {
            int? amount = GetAmountInput(consumable.CurrentStack, consumable.MaxStack, "consumable", "consume"); //gets amount
            if (amount == null) return; //if not specified return null

            if (consumable.CurrentStack >= amount) //if current stack more than amount
            {
                consumable.CurrentStack -= amount.Value; //remove amount from stack
                Console.WriteLine($"Consumed {amount} {consumable.Name}. Remaining: {consumable.CurrentStack}");
                if (consumable.CurrentStack == 0) //if stack is 0
                {
                    inventory.DropItem(consumable); //removes item
                }
            }
            else
            {
                Console.WriteLine("Not enough to consume that amount.");
            }
            PauseAndContinue();
        }

        //handles block pickup
        void HandleBlockPickup(InventoryClass inventory, Block block)
        {
            bool isTruee = true;
            while (isTruee)
            {
                int? amount = GetAmountInput(block.CurrentStack, block.MaxStack, "block", "pick up"); //gets the amount. can be either null or an int
                if (amount == null) //if amount not specified then exit loop
                {
                    isTruee = false;
                    continue;
                }

                if (block.CurrentStack >= amount) //if currentstack more or equal than amount selected
                {
                    block.CurrentStack -= amount.Value; //removes the amount from the stack
                    Block pickedBlock = new Block(block.Id, block.Name, block.BlockResistance, amount.Value); //copies a new block
                    Console.WriteLine($"You picked up {amount} {block.Name}. Remaining: {block.CurrentStack}"); //writes it in console
                    inventory.PickUpItem(pickedBlock); //picks up item
                    if (block.CurrentStack == 0) //if item reaches 0
                    {
                        itemsForPickingUp = itemsForPickingUp.Where(item => item != block).ToArray(); //removes the item
                    }
                    isTruee = false;
                }
                else
                {
                    Console.WriteLine($"Not enough blocks available. Press any key to try again");
                    Console.WriteLine($"Current stack {block.CurrentStack}/{block.MaxStack}");
                    Console.ReadLine();
                }
            }
        }

        //handles consumable pickup
        void handleConsumablePickup(InventoryClass inventory, Consumable consumable) //takes inventory and consumable and input
        {
            bool isTruee = true;
            while (isTruee) //while true
            {
                int? amount = GetAmountInput(consumable.CurrentStack, consumable.MaxStack, "consumable", "pick up"); //takes amount
                if (amount == null)  //if false set bool to false 
                {
                    isTruee = false;
                    continue;
                }

                if (consumable.CurrentStack >= amount) //if stack is bigger then amount run the logic
                {
                    consumable.CurrentStack -= amount.Value; //removes current stack with amount
                    Consumable pickedConsumable = new Consumable(consumable.Id, consumable.Name, amount.Value); //creates a new item consumable with the amount chosen
                    Console.WriteLine($"You picked up {amount} {consumable.Name}. Remaining: {consumable.CurrentStack}"); //displays it
                    inventory.PickUpItem(pickedConsumable); //add that item to li´st
                    if (consumable.CurrentStack == 0) //if stack is 0
                    {
                        itemsForPickingUp = itemsForPickingUp.Where(item => item != consumable).ToArray(); //remove it
                    }
                    isTruee = false;
                }
                else
                {
                    Console.WriteLine($"Not enough consumables available. Press any key to try again");
                    Console.WriteLine($"Current stack {consumable.CurrentStack}/{consumable.MaxStack}");
                    Console.ReadLine();
                }
            }
        }

        //handles dropping stackable items (blocks or consumables)
        void HandleStackableItemDrop(InventoryClass inventory, Item item, string itemType)
        {
            int? amount = GetAmountInput(item.CurrentStack, item.MaxStack, itemType, "drop"); //gets amount. int? tells that it can be either a null or int
            if (amount == null) return; //if null return nothing

            if (item.CurrentStack >= amount) //logic if currentstack is more than amount
            {
                item.CurrentStack -= amount.Value; //remove currentstack with amount value
                Console.WriteLine($"Dropped {amount} {item.Name}. Remaining: {item.CurrentStack}");
                if (item.CurrentStack == 0) //if item contains 0 of its items. then remove it from list
                {
                    inventory.DropItem(item); //deletes the item
                }
            }
            else
            {
                Console.WriteLine($"Not enough to drop that amount.");
            }
            PauseAndContinue();
        }

        //checks if the user wants to go back
        bool CheckGoBackInput(string input)
        {
            if (input.Equals("x", StringComparison.OrdinalIgnoreCase)) //if input is either capital or small X
            {
                Console.Clear();
                return false;
            }
            return true;
        }

        //pauses and waits for user input to continue
        void PauseAndContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

    }
    }
}