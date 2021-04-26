using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Response.users
{
    public class LoginResponse: BaseResponse
    {
        public bool LoginStatus { get; set; }
        public WebApi.Entities.Models.Customer Customer { get; set; }
    }
}
