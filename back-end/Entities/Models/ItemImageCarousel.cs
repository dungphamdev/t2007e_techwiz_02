using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class ItemImageCarousel
    {
        public int? ItemId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }

        public virtual Item Item { get; set; }
    }
}
