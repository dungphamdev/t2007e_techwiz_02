using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class StatusBilling
    {
        public StatusBilling()
        {
            Billings = new HashSet<Billing>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
    }
}
