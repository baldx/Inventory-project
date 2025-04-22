using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_project
{
    internal class InventoryClass
    {
        public object[,] Inventory; //[,] specifies that its a multi dimensional array

        public InventoryClass()
        {
            this.Inventory = new object[4, 9]; //creats a 2d array containing objects in a 4x9 grid 
        }

        public void Display() //logic to display inventory
        {
            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] == null || (Inventory[i, j] is Item item && (item.CurrentStack <= 0 || (item is Tool tool && tool.Durability <= 0)))) //checks for when the slot is empty, item has current stack of 0 and tool that has durability 0
                    {
                        Inventory[i, j] = null;
                        Console.Write("[ ] "); // shows empty slot
                    }
                    else
                    {
                        Item ExistingItem = (Item)Inventory[i, j];
                        if (ExistingItem is Item)
                        {
                            Console.Write("[" + ExistingItem.CurrentStack + " " + ExistingItem.Name + "] "); //displays the current item occupying it
                        }

                    }
                }
                Console.WriteLine(); // new line after each row
            }
        }

        public void PickUpItem(Item newItem)
        {
            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] is Item slotItem && slotItem.Name == newItem.Name) //checks if object in inventory contains Items class and if the item has the same name as the picked up item
                    {
                        int stackableAmount = slotItem.MaxStack - slotItem.CurrentStack; //checks how many items can be stacked for a specific item
                        if (stackableAmount > 0) //if stackable amount is over 0
                        {
                            int toAdd = Math.Min(newItem.CurrentStack, stackableAmount); //checks for how many items can fit in a stack
                            slotItem.CurrentStack += toAdd; //adds to the existing stack
                            newItem.CurrentStack -= toAdd; // removes added amount, reducing the amount left to be placed in another slot if needed
                            //Console.WriteLine($"Stacked {toAdd} {newItem.Name} in slot ({i},{j}). New stack: {slotItem.CurrentStack}/{slotItem.MaxStack}");

                            if (newItem.CurrentStack == 0) return; // all items stacked, exit
                        }
                    }
                }
            }

            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] == null) //if slot is empty
                    {
                        Inventory[i, j] = newItem; //add the new item
                        //Console.WriteLine($"Placed {newItem.Name} in slot ({i},{j}) with stack: {newItem.CurrentStack}/{newItem.MaxStack}"); //displays it
                        return;
                    }
                }
            }
        }

        public void DropItem(Item itemToDrop)
        {
            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] is Item currentItem && currentItem.Name == itemToDrop.Name) //if item equals to current item and name equals to chosen item name drop
                    {
                        Inventory[i, j] = null; //removes item from inventory
                        return;
                    }
                }
            }

            Console.WriteLine($"{itemToDrop.Name} not found in inventory.");
        }




        public void ChangeItemPositionTo(int OldX, int OldY, int NewX, int NewY)
        {
            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] != null && Inventory[OldX, OldY] != null) //checks for when the slot is not empty
                    {
                        if (Inventory[NewX, NewY] == null) //if new slot of inventory is empty
                        {
                            Inventory[NewX, NewY] = Inventory[i, j]; //switch old slot with the new slot
                            Inventory[i, j] = null; //change the old slots position with an empty one
                            return; //exists loop
                        } 
                        else
                        {
                            Console.WriteLine("Slot is already occupied"); //error handle if new slot is not empty
                            return; //exist loop
                        }
                    }

                }
            }
        }

        public void SortInventoryByName()
        {
            List<object> list = new List<object>(); //creates list. easier for dynamically resizing lists than arrays

            for (int i = 0; i < 4; i++) // rows
            {
                for (int j = 0; j < 9; j++) // columns
                {
                    if (Inventory[i, j] != null) //checks for when the slot is not empty
                    {
                        list.Add(Inventory[i, j]);//ads item to the list
                    }

                }
            }

            list.Sort((a, b) => a.ToString().CompareTo(b.ToString())); //sorts the array by comparing one string to another and alphabetically sorts it

            for (int i = 0; i < 4; i++) //logic to empty the inventory
            {
                for (int j = 0; j < 9; j++)
                {
                    Inventory[i, j] = null; //sets the position to empty
                }
            }

            int index = 0; 
            for (int i = 0; i < 4; i++)//logic to add the sorted list in the array
            {
                for (int j = 0; j < 9; j++)
                {
                    if (index < list.Count)
                    {
                        Inventory[i, j] = list[index]; //every position from (0, 0) adds an item from the sorted list
                        index++; 
                    }
                }
            }
        }

        public void ShowTotalSlots ()
        {
            Console.WriteLine(Inventory.Length); //displays the length of array, total slots
        }
    }
}
