using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Report.UserReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.UserReport.GetRecentUsers
{
    public class GetRecentUsersQueryHandler : IQueryHandler<GetRecentUsersQuery, RecentUsersReportDto>
    {
        private readonly ShopContext _context;

        public GetRecentUsersQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<RecentUsersReportDto> Handle(GetRecentUsersQuery request, CancellationToken cancellationToken)
        {
            var toDate = DateTime.UtcNow;
            var fromDate = toDate.AddDays(-request.Days);

            var count = await _context.Users.CountAsync(u => u.CreationDate >= fromDate, cancellationToken);

            return new RecentUsersReportDto
            {
                Count = count,
                FromDate = fromDate,
                ToDate = toDate
            };
        }
    }
}
