namespace WebApi.Controllers
{
    public class DeleteCustomerRequest
    {
        public int CustomerId { get; set; }
        public DeleteCustomerModel customer { get; set; }

        public class DeleteCustomerModel
        {
            public int CustomerId { get; set; }
        }
    }
}