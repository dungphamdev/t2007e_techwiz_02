using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? RestaurantId { get; set; }
        public string ItemDescription { get; set; }
        public decimal? ItemPrice { get; set; }
        public int? ItemCategoryId { get; set; }
        public bool? Active { get; set; }
        public string MainImagePath { get; set; }
        public string ImageType { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
