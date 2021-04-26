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
using WebApi.Messages.Response.restaurants;
using WebApi.Messages.Requests.restaurants;

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
                    var basePath = "upload\\restaurant";
                    var imagePath = basePath + "\\" + request.ImageName;

                    restaurant.RestaurantName = request.RestaurantName ?? "";
                    restaurant.RestaurantAddress = request.RestaurantAddress ?? "";
                    restaurant.RestaurantEmail = request.RestaurantEmail ?? "";
                    restaurant.RestaurantPhone = request.RestaurantPhone ?? "";
                    restaurant.Longitude = request.Longitude ?? null;
                    restaurant.Latitude = request.Latitude ?? null;
                    if (request.ImageName != null && request.ImageName.Length > 0)
                    {
                        restaurant.ImagePath = imagePath;
                        restaurant.ImageType = request.ContentType;
                    }
                    context.Restaurants.Update(restaurant);
                    if (request.ImageName != null && request.ImageName.Length > 0) { 
                        #region create image 
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var path = Path.Combine(contentRootPath, imagePath);
                    
                    UploadAdapter.UploadImage(basePath, request.ImageName, request.Base64Value);
                        #endregion
                    }
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
                List<Restaurant> listRestaurant = context.Restaurants.Where(w => w.Active == true).ToList() ?? new List<Restaurant>();
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
                        catch (Exception ex)
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

        [Route("restaurant/getRestaurantById")]
        [HttpPost]
        public GetRestaurantByIdResponse GetRestaurantById([FromBody] GetRestaurantByIdRequest request)
        {
            try
            {
                var restaurant = context.Restaurants.FirstOrDefault(f => f.RestaurantId == request.RestaurantId);
                var listItem = context.Items.Where(w => w.RestaurantId == request.RestaurantId).ToList() ?? new List<Item>();
                var listItemCategory = context.ItemCategories.ToList();

                var restaurantDetail = new RestaurantDetailModel();

                var listImageString = restaurant.ImagePath.Split('\\');
                string base64 = null;
                try
                {
                    var imagePath = restaurant.ImagePath.Replace('/', '\\');
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var path = Path.Combine(contentRootPath, imagePath);
                    byte[] b = System.IO.File.ReadAllBytes(path);
                    base64 = Convert.ToBase64String(b);
                }
                catch (Exception ex)
                {

                }

                restaurantDetail.RestaurantId = restaurant.RestaurantId;
                restaurantDetail.RestaurantName = restaurant.RestaurantName;
                restaurantDetail.RestaurantAddress = restaurant.RestaurantAddress;
                restaurantDetail.RestaurantEmail = restaurant.RestaurantEmail;
                restaurantDetail.RestaurantPhone = restaurant.RestaurantPhone;
                restaurantDetail.Longitude = restaurant.Longitude;
                restaurantDetail.Latitude = restaurant.Latitude;
                restaurantDetail.ImagePath = restaurant.ImagePath;
                restaurantDetail.ImageName = listImageString.LastOrDefault() ?? "";
                restaurantDetail.ImageSrc = restaurant.ImageType + "," + base64;

                listItem?.ForEach(item =>
                {
                    var listItemCategoryByItem = listItemCategory.Where(w => w.CategoryId == item.ItemCategoryId).ToList();
                    var listImageString = item.MainImagePath.Split('\\');
                    string base64 = null;
                    try
                    {
                        var imagePath = item.MainImagePath.Replace('/', '\\');
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var path = Path.Combine(contentRootPath, imagePath);
                        byte[] b = System.IO.File.ReadAllBytes(path);
                        base64 = Convert.ToBase64String(b);
                    }
                    catch (Exception ex)
                    {
                    }

                    restaurantDetail.ListItem.Add(new Models.items.ItemModel
                    {
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        RestaurantId = item.RestaurantId,
                        ItemDescription = item.ItemDescription,
                        ItemPrice = item.ItemPrice,
                        ItemCategoryId = item.ItemCategoryId,
                        MainImagePath = item.MainImagePath,
                        ImageType = item.ImageType,

                        ImageName = listImageString.LastOrDefault() ?? "",
                        ImageSrc = item.ImageType + "," + base64,

                        RestaurantLabel = restaurant?.RestaurantName ?? "",
                        ItemCategoryLabel = String.Join(", ", listItemCategoryByItem.Select(w => w.CategoryName).ToList())
                    });
                });

                return new GetRestaurantByIdResponse
                {
                    RestaurantDetail = restaurantDetail,
                    StatusCode = (int)HttpStatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new GetRestaurantByIdResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }



    }
}
