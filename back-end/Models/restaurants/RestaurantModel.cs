using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.restaurants
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPhone { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public bool? Active { get; set; }
        public string ImagePath { get; set; }

        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
    }
}
