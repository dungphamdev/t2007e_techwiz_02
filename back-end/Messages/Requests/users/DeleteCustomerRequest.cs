namespace WebApi.Controllers
{
    public class DeleteCustomerRequest
    {
        public DeleteCustomerModel customer { get; set; }

        public class DeleteCustomerModel
        {
            public int CustomerId { get; set; }
        }
    }
}