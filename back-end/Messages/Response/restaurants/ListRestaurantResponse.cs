using System.Collections.Generic;
using WebApi.Entities.Models;
using WebApi.Messages.Response;
using WebApi.Models.restaurants;

namespace WebApi.Controllers
{
    public class ListRestaurantResponse:BaseResponse
    {
        public List<RestaurantModel> list { get; set; }
    }
}