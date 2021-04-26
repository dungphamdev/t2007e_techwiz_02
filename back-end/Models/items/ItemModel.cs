using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities.Models;

namespace WebApi.Models.items
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? RestaurantId { get; set; }
        public string ItemDescription { get; set; }
        public decimal? ItemPrice { get; set; }
        public int? ItemCategoryId { get; set; }
        public bool? Active { get; set; }
        public string MainImagePath { get; set; }
        public string ImageType { get; set; }

        public string ImageName { get; set; }
        public string ImageSrc { get; set; }

        public Restaurant RestaurantInfor { get; set; }
        public List<ItemCategory> ListItemCategory { get; set; }

        public string RestaurantLabel { get; set; }
        public string ItemCategoryLabel { get; set; }

        public ItemModel()
        {
            this.ListItemCategory = new List<ItemCategory>();
        }
    }
}
