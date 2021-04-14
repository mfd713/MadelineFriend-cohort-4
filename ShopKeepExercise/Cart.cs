using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Cart
    {
        private const int _LIMIT = 5;
        public Dictionary<Item,int> Inventory { get; set; }
        public int ProtectionLvl { get; set; }
        public bool IsFull { get {
                return Inventory.Count == _LIMIT;
            } private set { 
            if(Inventory.Count == _LIMIT)
                {
                    IsFull = true;
                }
                else
                {
                    IsFull = false;
                }
            } }
    }
}
