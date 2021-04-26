using System.Collections.Generic;
using WebApi.Entities.Models;
using WebApi.Messages.Response;
using WebApi.Models.items;

namespace WebApi.Controllers
{
    public class ListItemResponse:BaseResponse
    {
        public List<ItemModel> list { get; set; }
    }
}