using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.ShopReport.FinancialReport.MonthlyIncome
{
    public class GetMonthlyIncomeQueryHandler : IQueryHandler<GetMonthlyIncomeQuery, List<MonthlyIncomeReportDto>>
    {
        private readonly ShopContext _context;

        public GetMonthlyIncomeQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyIncomeReportDto>> Handle(GetMonthlyIncomeQuery request, CancellationToken cancellationToken)
        {
            var end = DateTime.Now;
            var start = end.AddMonths(-request.MonthCount);

            var data = await _context.Orders
                .Where(o => o.Status == OrderStatus.Finally && o.LastUpdate >= start).ToListAsync(cancellationToken);

            var result = data
                .GroupBy(o => o.LastUpdate!.Value.ToString("yyyy-MM"))
                .Select(g => new MonthlyIncomeReportDto
                {
                    Month = g.Key,
                    TotalIncome = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return result;
        }
    }
}
