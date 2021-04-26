using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Requests.users
{
    public class UpdateUserRequest
    {
        public UpdateUserModel user { get; set; }
    }

    public class UpdateUserModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
