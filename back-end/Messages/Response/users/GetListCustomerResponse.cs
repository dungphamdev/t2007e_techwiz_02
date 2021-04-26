using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Response.users
{
    public class GetListCustomerResponse: BaseResponse
    {
        public List<WebApi.Entities.Models.Customer> ListCustomer { get; set; }
    }
}
