using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Shopkeeper
    {
        public Shopkeeper(string name, Cart cart)
        {
            Name = name;
            Cart = cart;
            Distance = 0;
        }

        public string Name { get; private set; }
        public Cart Cart { get; private set; }
        public int Distance { get; set; }


    }
}