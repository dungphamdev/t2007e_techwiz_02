using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.restaurants;

namespace WebApi.Messages.Response.restaurants
{
    public class GetRestaurantByIdResponse: BaseResponse
    {
        public RestaurantDetailModel RestaurantDetail { get; set; }
    }
}
