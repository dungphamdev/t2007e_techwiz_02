using System.Collections.Generic;

namespace WebApi.Controllers
{
    public class CreateItemRequest
    {
        public string ItemName { get; set; }
        public int? RestaurantId { get; set; }
        public string ItemDescription { get; set; }
        public decimal? ItemPrice { get; set; }
        public int? ItemCategoryId { get; set; }
        public string MainImagePath { get; set; }

        public string Base64Value { get; set; }
        public string ImageName { get; set; }
        public string ContentType { get; set; }
    }
}