using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.admin
{
    public class OrderDetailsModel
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ItemId { get; set; }
        public int? ItemQty { get; set; }
    }
}
