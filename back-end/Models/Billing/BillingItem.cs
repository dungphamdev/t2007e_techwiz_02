using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Billing
{
    public class BillingItem
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public decimal? BillAmout { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? StatusId { get; set; }

        public string CustomerName { get; set; }
        public string RestaurantName { get; set; }
        public string StatusName { get; set; }
    }
}
