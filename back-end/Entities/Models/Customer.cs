using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Billings = new HashSet<Billing>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailId { get; set; }
        public string CustomerContactPhone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CustomerAddress { get; set; }
        public bool? Active { get; set; }
        public bool IsStaff { get; set; }
        public int? RestaurantId { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
    }
}
