using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Entities.Models;
using WebApi.Messages.Response.admin;
using WebApi.Models.admin;

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

        [Route("admin/bills/list")]
        [HttpPost]
        public GetListBillingResponse GetAllBill()
        {
            try
            {
                var listBillingEntity = context.Billings.ToList();
                var listOrderDetailsEntity = context.OrderDetails.ToList();

                var listBilling = new List<BillingModel>();

                listBillingEntity?.ForEach(bill =>
                {

                    var detailEntity = listOrderDetailsEntity.Where(w => w.OrderId == bill.OrderId).ToList() ?? new List<OrderDetail>();

                    var orderDetail = new List<OrderDetailsModel>();

                    detailEntity?.ForEach(e =>
                    {
                        orderDetail.Add(new OrderDetailsModel
                        {

                            OrderDetailId = e.OrderDetailId,
                            OrderId = e.OrderId,
                            ItemId = e.ItemId,
                            ItemQty = e.ItemQty
                        });
                    });

                    listBilling.Add(new BillingModel
                    {
                        OrderId = bill.OrderId,
                        CustomerId = bill.CustomerId,
                        RestaurantId = bill.RestaurantId,
                        BillAmout = bill.BillAmout,
                        OrderDate = bill.OrderDate,
                        StatusId = bill.StatusId,
                        //TODO: Customer label, Restaurant label, Status label
                        ListOrderDetails = orderDetail
                    });
                });

                return new GetListBillingResponse
                {
                    ListBilling = listBilling
                };
            }
            catch (Exception e)
            {
                return new GetListBillingResponse
                {
                };
            }
        }
    }
}
