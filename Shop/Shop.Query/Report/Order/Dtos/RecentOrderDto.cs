using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.Order.Dtos
{
    public class RecentOrderDto : BaseDto
    {
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public decimal PaidMoney { get; set; }
        public DateTime OrderDate { get; set; }

        public List<RecentOrderItemDto> Items { get; set; }
    }

    public class RecentOrderItemDto
    {
        public string ProductTitle { get; set; }
        public string ProductImageName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Count;
    }
}