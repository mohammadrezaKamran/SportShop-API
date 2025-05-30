using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.FinancialReport.TotalIncome
{
    public class GetTotalIncomeQueryHandler : IQueryHandler<GetTotalIncomeQuery, IncomeReportDto>
    {
        private readonly ShopContext _context;

        public GetTotalIncomeQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<IncomeReportDto> Handle(GetTotalIncomeQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                  .Where(o => o.Status == OrderStatus.Finally).ToListAsync(cancellationToken);

            var total = orders.Sum(o => o.TotalPrice);

            return new IncomeReportDto
            {
                TotalIncome = total,
                TotalOrders = orders.Count
            };
        }
    }
}
