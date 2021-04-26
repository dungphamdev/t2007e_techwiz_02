using System.Collections.Generic;
using WebApi.Entities.Models;
using WebApi.Messages.Response;

namespace WebApi.Controllers
{
    public class ListCategoryResponse:BaseResponse
    {
        public List<ItemCategory> list { get; set; }
    }
}