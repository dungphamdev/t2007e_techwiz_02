using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities.Models;

namespace WebApi.Messages.Response.users
{
    public class GetAllUsersResponse:BaseResponse
    {
        public List<User> ListUsers { get; set; }
    }
}
