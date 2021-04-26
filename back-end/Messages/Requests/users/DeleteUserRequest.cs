using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Requests.users
{
    public class DeleteUserRequest
    {
        public Guid UserId { get; set; }
    }
}
