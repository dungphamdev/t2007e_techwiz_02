using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Messages.Requests.item
{
    public class GetImageItemByIdRequest
    {
        public List<int> ListItemId { get; set; }
    }
}
