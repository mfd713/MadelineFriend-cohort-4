using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class ProjectItem
    {
        public int ProjectId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }

        public Item Item { get; set; }
        public Project Project { get; set; }
    }
}
