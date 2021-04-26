using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Billings = new HashSet<Billing>();
            Items = new HashSet<Item>();
        }

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPhone { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public bool? Active { get; set; }
        public string ImagePath { get; set; }
        public string ImageType { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
