using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.admin;

namespace WebApi.Messages.Response.admin
{
    public class GetListBillingResponse
    {
        public List<BillingModel> ListBilling { get; set; }
    }
}
