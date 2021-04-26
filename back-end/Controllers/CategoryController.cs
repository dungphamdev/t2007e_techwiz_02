using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Entities.Models;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private DB_CloudKitchenContext context;

        public CategoryController(DB_CloudKitchenContext db)
        {
            context = db;
        }

        [Route("category/list")]
        [HttpPost]
        public ListCategoryResponse ListCategory()
        {
            try
            {
                List<ItemCategory> listCategory = context.ItemCategories.ToList() ?? new List<ItemCategory>();
                if (listCategory != null)
                {
                    context.Dispose();
                    return new ListCategoryResponse { list = listCategory, StatusCode = (int)HttpStatusCode.OK };
                }
                else
                {
                    return new ListCategoryResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception e)
            {
                return new ListCategoryResponse
                {                     
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("category/create")]
        [HttpPost]
        public CreateCategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                if (request != null)
                {
                    //var newCategory = new ItemCategory
                    //{                        
                    //    ParentId = request.ParentId ?? null,
                    //    CategoryName = request.CategoryName ?? ""     
                    //};

                    var newCategory = new ItemCategory();
                    newCategory.ParentId = request.ParentId ?? null;
                       newCategory.CategoryName = request.CategoryName ?? "";
                    context.ItemCategories.Add(newCategory);
                    context.SaveChanges();
                    context.Dispose();
                }
                else
                {
                    return new CreateCategoryResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new CreateCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new CreateCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("category/delete")]
        [HttpPost]
        public DeleteCategoryResponse CreateCategory(DeleteCategoryRequest request)
        {
            try
            {
                if (request != null)
                {
                    ItemCategory item = context.ItemCategories.FirstOrDefault(p => p.CategoryId == request.CategoryId && p.Active == true);
                    if(item != null)
                    {
                        item.Active = false;
                        context.ItemCategories.Update(item);
                        context.SaveChanges();
                        context.Dispose();
                    }                   
                }
                else
                {
                    return new DeleteCategoryResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new DeleteCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new DeleteCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("category/update")]
        [HttpPost]
        public UpdateCategoryResponse CreateCategory(UpdateCategoryRequest request)
        {
            try
            {
                if (request != null)
                {
                    ItemCategory item = context.ItemCategories.FirstOrDefault(p => p.CategoryId == request.CategoryId && p.Active == true);
                    if (item != null)
                    {
                        item.CategoryName = request.CategoryName ?? "";
                        item.ParentId = request.ParentId ?? null;
                        context.ItemCategories.Update(item);
                        context.SaveChanges();
                        context.Dispose();
                    }
                }
                else
                {
                    return new UpdateCategoryResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return new UpdateCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new UpdateCategoryResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
 