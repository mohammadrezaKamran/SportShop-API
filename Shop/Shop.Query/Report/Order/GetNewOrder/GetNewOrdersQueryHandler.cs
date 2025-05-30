using Common.Query;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Shop.Query.Report.Order.GetNewOrder
{
    internal class GetNewOrdersQueryHandler : IRequestHandler<GetNewOrdersQuery, long>
    {
        private readonly ShopContext _context;

        public GetNewOrdersQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(GetNewOrdersQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Orders
                .Where(o => o.CreationDate.Date == today && o.Status == OrderStatus.Finally)
                .CountAsync(cancellationToken);
        }
    }
}
