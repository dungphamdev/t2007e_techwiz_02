namespace WebApi.Controllers
{
    public class CustomerCancelRequest
    {
        public int CustomerId { get;  set; }
        public int OrderId { get;  set; }
    }
}