using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Orders.GetOrderItems
{
    public class GetOrderItemsQuery : IQuery<List<OrderItemDto>>
    {
        public long UserId {  get; set; }
    }
}
