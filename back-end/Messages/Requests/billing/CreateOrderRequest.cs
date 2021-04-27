using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Requests.billing
{
    public class CreateOrderRequest
    {
        public CreateBillingItemModel BillingItem { get; set; }
    }

    public class CreateBillingItemModel
    {
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public decimal? BillAmout { get; set; }
        public DateTime? OrderDate { get; set; }

        public List<CreateBillingOrderDetailModel> OrderDetail { get; set; }

        public CreateBillingItemModel()
        {
            this.OrderDetail = new List<CreateBillingOrderDetailModel>();
        }
    }

    public class CreateBillingOrderDetailModel
    {
        public int? ItemId { get; set; }
        public int? ItemQty { get; set; }
    }
}
