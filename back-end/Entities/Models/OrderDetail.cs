using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ItemId { get; set; }
        public int? ItemQty { get; set; }

        public virtual Item Item { get; set; }
        public virtual Billing Order { get; set; }
    }
}
