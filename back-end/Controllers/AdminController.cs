using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Entities.Models;
using WebApi.Messages.Requests.billing;
using WebApi.Messages.Response;
using WebApi.Messages.Response.billing;
using WebApi.Models.admin;
using WebApi.Models.Billing;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private DB_CloudKitchenContext context;

        public AdminController(DB_CloudKitchenContext db)
        {
            context = db;
        }

        [Route("billing/list")]
        [HttpPost]
        public GetListBillingResponse GetListBilling([FromBody] GetListBillingRequest request)
        {
            try
            {
                var listCustomer = context.Customers.ToList();
                var listRestaurant = context.Restaurants.ToList();
                var listStatus = context.StatusBillings.ToList();
                var listBilling = context.Billings.ToList();

                var listBillingResponse = new List<BillingItem>();

                listBilling?.ForEach(bill =>
                {
                    var customer = listCustomer.FirstOrDefault(f => f.CustomerId == bill.CustomerId);
                    var restaurant = listRestaurant.FirstOrDefault(f => f.RestaurantId == bill.RestaurantId);
                    var status = listStatus.FirstOrDefault(f => f.StatusId == bill.StatusId);

                    listBillingResponse.Add(new BillingItem
                    {
                        OrderId = bill.OrderId,
                        CustomerId = bill.CustomerId,
                        RestaurantId = bill.RestaurantId,
                        BillAmout = bill.BillAmout,
                        OrderDate = bill.OrderDate,
                        StatusId = bill.StatusId,

                        CustomerName = customer?.CustomerName ?? "",
                        RestaurantName = restaurant.RestaurantName ?? "",
                        StatusName = status.StatusName ?? ""
                    });
                });

                return new GetListBillingResponse
                {
                    ListBillingItem = listBillingResponse.OrderBy(w => w.StatusId).ToList(),
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new GetListBillingResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("billing/aproval")]
        [HttpPost]
        public ApprovalBillingResponse ApprovalBilling([FromBody] ApprovalBillingRequest request)
        {
            try
            {

                var billing = context.Billings.FirstOrDefault(f => f.OrderId == request.OrderId);

                 if (billing != null)
                {
                    billing.StatusId = 2;
                    context.Billings.Update(billing);
                    context.SaveChanges();
                }

                return new ApprovalBillingResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ApprovalBillingResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Route("billing/reject")]
        [HttpPost]
        public RejectBillingResponse RejectBilling([FromBody] RejectBillingRequest request)
        {
            try
            {

                var billing = context.Billings.FirstOrDefault(f => f.OrderId == request.OrderId);

                if (billing != null)
                {
                    billing.StatusId = 3;
                    context.Billings.Update(billing);
                    context.SaveChanges();
                }

                return new RejectBillingResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new RejectBillingResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }


        [Route("billing/createOrder")]
        [HttpPost]
        public CreateOrderResponse CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {

                var newOrder = new Billing();
                
                newOrder.CustomerId = request.BillingItem.CustomerId;
                newOrder.RestaurantId = request.BillingItem.RestaurantId;
                newOrder.BillAmout = request.BillingItem.BillAmout;
                newOrder.OrderDate = DateTime.Now;
                newOrder.StatusId = 1;

                context.Billings.Add(newOrder);
                context.SaveChanges();

                var newListOrder = new List<OrderDetail>();

                request.BillingItem.OrderDetail?.ForEach(item =>
                {
                    newListOrder.Add(new OrderDetail
                    {
                        OrderId = newOrder.OrderId,
                        ItemQty = item.ItemQty,
                        ItemId = item.ItemId
                    });
                });

                context.OrderDetails.AddRange(newListOrder);
                context.SaveChanges();

                return new CreateOrderResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new CreateOrderResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
