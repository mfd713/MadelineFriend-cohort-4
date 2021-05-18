using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            Items = new HashSet<Item>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
        public ICollection<Category> InverseParentCategory { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
