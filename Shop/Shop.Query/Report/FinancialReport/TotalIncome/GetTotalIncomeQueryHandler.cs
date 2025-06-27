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
           var now = DateTime.UtcNow;

            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.Status == OrderStatus.Finally)
                .ToListAsync(cancellationToken);

            var totalIncome = orders.Sum(o => o.TotalPrice);

            var dailyIncome = orders
                .Where(o => o.CreationDate.Date == now.Date)
                .Sum(o => o.TotalPrice);

            var monthlyIncome = orders
                .Where(o => o.CreationDate.Year == now.Year && o.CreationDate.Month == now.Month)
                .Sum(o => o.TotalPrice);

            var sixMonthAgo = now.AddMonths(-5); 
            var sixMonthlyIncome = orders
                .Where(o => o.CreationDate >= new DateTime(sixMonthAgo.Year, sixMonthAgo.Month, 1))
                .Sum(o => o.TotalPrice);

            return new IncomeReportDto()
            {
                TotalIncome = totalIncome,
                DailyIncome = dailyIncome,
                MonthlyIncome = monthlyIncome,
                SixMonthlyIncome = sixMonthlyIncome
            };
                
               
        }        

    }

}


