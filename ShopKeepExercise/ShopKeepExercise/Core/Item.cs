using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Item
    {
        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public int Value { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Name == item.Name &&
                   Value == item.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }
    }
}