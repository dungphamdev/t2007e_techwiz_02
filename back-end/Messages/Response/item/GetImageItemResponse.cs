using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.items;

namespace WebApi.Messages.Response.item
{
    public class GetImageItemResponse: BaseResponse
    {
        public List<ItemModel> ListItem { get; set; }
    }
}
