namespace WebApi.Controllers
{
    public class CustomerOrderRequest
    {
        public int CustomerId { get;  set; }
        public int RestaurantId { get;  set; }
        public decimal? BillAmount { get;  set; }
        public string OrderLocation { get;  set; }
        public string OrderItemName { get;  set; }
        public int OrderItemQty { get;  set; }
    }
}