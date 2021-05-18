using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Item
    {
        public Item()
        {
            ProjectItems = new HashSet<ProjectItem>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal PricePerUnit { get; set; }

        public Category Category { get; set; }
        public Unit Unit { get; set; }
        public ICollection<ProjectItem> ProjectItems { get; set; }
    }
}
