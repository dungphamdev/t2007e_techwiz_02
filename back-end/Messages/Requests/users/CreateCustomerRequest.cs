namespace WebApi.Controllers
{
    public class CreateCustomerRequest
    {
        public CreateCustomerModel customer { get; set; }

        public class CreateCustomerModel
        {
            public string CustomerName { get; set; }
            public string CustomerEmailId { get; set; }
            public string CustomerContactPhone { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string CustomerAddress { get; set; }
            public bool IsStaff { get; set; }
            public int? RestaurantId { get; set; }
        }
    }
}