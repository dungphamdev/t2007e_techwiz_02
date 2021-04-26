using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services;
using WebApi.Messages.Requests.users;
using WebApi.Messages.Response.users;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebApi.Helpers;
using WebApi.Models.restaurants;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private DB_CloudKitchenContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RestaurantController(DB_CloudKitchenContext db, IWebHostEnvironment webHostEnvironment)
        {
            context = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("restaurant/create")]
        [HttpPost]
        public CreateRestaurantResponse CreateRestaurant([FromBody] CreateRestaurantRequest request)
        {
            try
            {
                if (request != null)
                {
                    var basePath = "upload\\restaurant";
                    var imagePath = basePath + "\\" + request.ImageName;

                    var newRestaurant = new Restaurant
                    {
                        RestaurantName = request.RestaurantName ?? "",
                        RestaurantAddress = request.RestaurantAddress ?? "",
                        RestaurantEmail = request.RestaurantEmail ?? "",
                        RestaurantPhone = request.RestaurantPhone ?? "",
                        Longitude = request.Longitude ?? null,
                        Latitude = request.Latitude ?? null,
                        ImagePath = imagePath,
                        ImageType = request.ContentType

                    };
                    context.Restaurants.Add(newRestaurant);

                    #region create image 
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var path = Path.Combine(contentRootPath, imagePath);
                    UploadAdapter.UploadImage(basePath, request.ImageName, request.Base64Value);
                    #endregion
                    context.SaveChanges();
                    context.Dispose();
                }
                else
                {
                    return new CreateRestaurantResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new CreateRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new CreateRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("restaurant/update")]
        [HttpPost]
        public UpdateRestaurantResponse UpdateRestaurant([FromBody] UpdateRestaurantRequest request)
        {
            try
            {
                var restaurant = context.Restaurants.FirstOrDefault(p => p.RestaurantId == request.RestaurantId && p.Active == true);
                if (restaurant != null)
                {
                    restaurant.Latitude = request.Latitude;
                    restaurant.Longitude = request.Longitude;
                    restaurant.RestaurantAddress = request.RestaurantAddress;
                    restaurant.RestaurantEmail = request.RestaurantEmail;
                    restaurant.RestaurantPhone = request.RestaurantPhone;
                    restaurant.RestaurantName = request.RestaurantName;
                    context.Restaurants.Update(restaurant);
                    context.SaveChanges();
                    context.Dispose();
                }
                else
                {
                    return new UpdateRestaurantResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new UpdateRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new UpdateRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("restaurant/list")]
        [HttpPost]
        public ListRestaurantResponse ListRestaurant()
        {
            try
            {
                List<Restaurant> listRestaurant = context.Restaurants.ToList() ?? new List<Restaurant>();
                if (listRestaurant != null)
                {
                    var listRestaurantResponse = new List<RestaurantModel>();

                    listRestaurant?.ForEach(e =>
                    {
                        var listImageString = e.ImagePath.Split('\\');
                        string base64 = null;
                        try
                        {
                            var imagePath = e.ImagePath.Replace('/', '\\');
                            string contentRootPath = _webHostEnvironment.ContentRootPath;
                            var path = Path.Combine(contentRootPath, imagePath);
                            byte[] b = System.IO.File.ReadAllBytes(path);
                            base64 = Convert.ToBase64String(b);
                        }
                        catch(Exception ex)
                        {
                           
                        }

                        listRestaurantResponse.Add(new RestaurantModel
                        {
                            RestaurantId = e.RestaurantId,
                            RestaurantName = e.RestaurantName,
                            RestaurantAddress = e.RestaurantAddress,
                            RestaurantEmail = e.RestaurantEmail,
                            RestaurantPhone = e.RestaurantPhone,
                            Longitude = e.Longitude,
                            Latitude = e.Latitude,
                            ImagePath = e.ImagePath,
                            ImageName = listImageString.LastOrDefault() ?? "",
                            ImageSrc = e.ImageType + "," + base64
                        });
                    }
                    );

                    return new ListRestaurantResponse
                    {
                        list = listRestaurantResponse,
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ListRestaurantResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception e)
            {
                return new ListRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("restaurant/delete")]
        [HttpPost]
        public DeleteRestaurantResponse DeleteRestaurant([FromBody] DeleteRestaurantRequest request)
        {
            try
            {
                var Restaurant = context.Restaurants.FirstOrDefault(p => p.RestaurantId == request.RestaurantId && p.Active == true);
                if (Restaurant != null)
                {
                    Restaurant.Active = false;
                    context.Update(Restaurant);
                    context.SaveChanges();
                    context.Dispose();
                    return new DeleteRestaurantResponse
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    return new DeleteRestaurantResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception e)
            {
                return new DeleteRestaurantResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
