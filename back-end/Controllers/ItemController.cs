using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Entities.Models;
using WebApi.Helpers;
using WebApi.Models.items;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private DB_CloudKitchenContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(DB_CloudKitchenContext db, IWebHostEnvironment webHostEnvironment)
        {
            context = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("item/list")]
        [HttpPost]
        public ListItemResponse ListItem()
        {
            try
            {
                List<Item> listItem = context.Items.ToList() ?? new List<Item>();

                var listRestaurant = context.Restaurants.ToList() ?? new List<Restaurant>();
                var listItemCategory = context.ItemCategories.ToList();

                if (listItem != null)
                {
                    //context.Dispose();
                    //return new ListItemResponse { list = listItem, StatusCode = (int)HttpStatusCode.OK };

                    var listResponse = new List<ItemModel>();

                    listItem?.ForEach(item =>
                    {
                        var restaurant = listRestaurant.FirstOrDefault(f => f.RestaurantId == item.RestaurantId);
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

                        listResponse.Add(new ItemModel
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

                            //RestaurantInfor = restaurant,
                            //ListItemCategory = listItemCategoryByItem

                            RestaurantLabel = restaurant?.RestaurantName ?? "",
                            ItemCategoryLabel = String.Join(", ", listItemCategoryByItem.Select(w => w.CategoryName).ToList())
                        });
                    });

                    var result = new ListItemResponse
                    {
                        list = listResponse,
                        StatusCode = (int)HttpStatusCode.OK
                    };

                    return result;
                }
                else
                {
                    return new ListItemResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception e)
            {
                return new ListItemResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("item/create")]
        [HttpPost]
        public CreateItemResponse CreateItem(CreateItemRequest request)
        {
            try
            {
                if (request != null)
                {
                    var basePath = "upload\\item";
                    var imagePath = basePath + "\\" + request.ImageName;

                    var newItem = new Item
                    {
                        ItemName = request.ItemName ?? null,
                        RestaurantId = request.RestaurantId ?? null,
                        ItemDescription = request.ItemDescription ?? "",
                        ItemPrice = request.ItemPrice ?? null,
                        ItemCategoryId = request.ItemCategoryId ?? null,
                        MainImagePath = imagePath,
                        ImageType = request.ContentType
                    };
                    context.Items.Add(newItem);
                    context.SaveChanges();
                    context.Dispose();

                    #region create image 
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var path = Path.Combine(contentRootPath, imagePath);
                    UploadAdapter.UploadImage(basePath, request.ImageName, request.Base64Value);
                    #endregion
                }
                else
                {
                    return new CreateItemResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new CreateItemResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new CreateItemResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("item/update")]
        [HttpPost]
        public UpdateItemResponse UpdateItem(UpdateItemRequest request)
        {
            try
            {
                if (request != null)
                {
                    Item item = context.Items.FirstOrDefault(p => p.ItemId == request.ItemId && p.Active == true);
                    if(item != null)
                    {
                        item.ItemCategoryId = request.ItemCategoryId ?? null;
                        item.ItemDescription = request.ItemDescription ?? "";
                        item.ItemName = request.ItemName ?? "";
                        item.ItemPrice = request.ItemPrice ?? null;
                        item.MainImagePath = request.MainImagePath ?? "";
                        item.RestaurantId = request.RestaurantId ?? null;
                        context.Update(item);
                        context.SaveChanges();
                        context.Dispose();
                    }
                    else
                    {
                        return new UpdateItemResponse
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                    }
                }
                else
                {
                    return new UpdateItemResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new UpdateItemResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new UpdateItemResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("item/delete")]
        [HttpPost]
        public DeleteItemResponse DeleteItem(DeleteItemRequest request)
        {
            try
            {
                if (request != null)
                {
                    Item item = context.Items.FirstOrDefault(p => p.ItemId == request.ItemId && p.Active == true);
                    if (item != null)
                    {
                        item.Active = false;
                        context.Update(item);
                        context.SaveChanges();
                        context.Dispose();
                    }
                    else
                    {
                        return new DeleteItemResponse
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                    }
                }
                else
                {
                    return new DeleteItemResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new DeleteItemResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new DeleteItemResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
