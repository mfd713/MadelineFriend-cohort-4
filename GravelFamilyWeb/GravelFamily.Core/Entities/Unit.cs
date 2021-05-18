using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Unit
    {
        public Unit()
        {
            Items = new HashSet<Item>();
        }

        public int UnitId { get; set; }
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
