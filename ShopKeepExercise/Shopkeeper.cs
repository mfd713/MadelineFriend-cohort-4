using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Shopkeeper
    {
        public string Name { get; private set; }
        public Cart Cart { get; private set; }
        public int Distance { get; set; }
        public int Gold { get; set; }
    }
}
