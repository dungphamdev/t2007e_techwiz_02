using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Billing;

namespace WebApi.Messages.Response
{
    public class GetListBillingResponse: BaseResponse
    {
        public List<BillingItem> ListBillingItem { get; set; }
    }
}
