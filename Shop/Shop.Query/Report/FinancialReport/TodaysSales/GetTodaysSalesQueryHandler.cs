using Common.Application;
using Common.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.FinancialReport.TodaysSales
{
    internal class GetTodaysSalesQueryHandler : IRequestHandler<GetTodaysSalesQuery, decimal>
    {
        private readonly ShopContext _context;

        public GetTodaysSalesQueryHandler(ShopContext context)
        {
            _context = context;
        }

       async Task<decimal> IRequestHandler<GetTodaysSalesQuery, decimal>.Handle(GetTodaysSalesQuery request, CancellationToken cancellationToken)
        {     
            try
            {
                var today = DateTime.UtcNow.Date;

                var sales = await _context.Orders
                    .Where(o => o.CreationDate.Date == today && o.Status == OrderStatus.Finally)
                    .SumAsync(o => (decimal?)o.TotalPrice, cancellationToken);

                return sales ?? 0m;
            }
            catch (Exception ex)
            {
                return 0m;
            }
        }
    }
}
