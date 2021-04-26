using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.admin
{
    public class BillingModel
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public decimal? BillAmout { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? StatusId { get; set; }

        public List<OrderDetailsModel> ListOrderDetails { get; set; }

        public BillingModel()
        {
            this.ListOrderDetails = new List<OrderDetailsModel>();
        }
    }
}
