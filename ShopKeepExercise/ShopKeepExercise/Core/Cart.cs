using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Cart
    {
        private const int _LIMIT = 5;
        public Dictionary<Item, int> Inventory { get; set; }
        public int ProtectionLvl { get; set; }
        public int Gold { get; set; }

        public bool IsFull
        {
            get
            {
                return Inventory.Count == _LIMIT;
            }
            private set
            {
                if (Inventory.Count == _LIMIT)
                {
                    IsFull = true;
                }
                else
                {
                    IsFull = false;
                }
            }
        }

        public void RemoveInventoryItem(Dictionary<Item, int> inventory, Item toRemove)
        {
            if (inventory[toRemove] != 0)
            {
                inventory[toRemove]--;
                if (inventory[toRemove] == 0)
                {
                    inventory.Remove(toRemove);
                    Console.WriteLine($"No more {toRemove.Name} in your inventory");
                }
                else
                {
                    Console.WriteLine($"You now have {inventory[toRemove]} {toRemove.Name} in your inventory");
                }
            }
            else
            {
                Console.WriteLine("Item not found");
            }

        }
    }
}