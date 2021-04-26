using System;
using System.Collections.Generic;
using WebApi.Messages.Response;

namespace WebApi.Controllers
{
    public class AdminGetAllBillResponse:BaseResponse
    {
        public List<AdminGetAllBillModel> list { get; set; }     
    }
    public class AdminGetAllBillModel
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderLocation { get; set; }
        public string OrderItemName { get; set; }
        public int OrderItemQty { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public decimal? BillAmount { get; set; }
        public DateTime? Date { get; set; }
        public int Status { get; set; }
    }
}