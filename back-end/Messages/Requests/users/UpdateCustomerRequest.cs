namespace WebApi.Controllers
{
    public class UpdateCustomerRequest
    {
        public UpdateCustomerModel customer { get; set; }

        public class UpdateCustomerModel
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmailId { get; set; }
            public string CustomerContactPhone { get; set; }
            public string CustomerAddress { get; set; }
        }
    }
}