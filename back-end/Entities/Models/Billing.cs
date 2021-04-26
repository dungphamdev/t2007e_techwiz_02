using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class Billing
    {
        public Billing()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public decimal? BillAmout { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? StatusId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual StatusBilling Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
