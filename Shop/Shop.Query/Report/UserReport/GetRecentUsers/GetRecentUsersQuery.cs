using Common.Query;
using Shop.Query.Report.UserReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.UserReport.GetRecentUsers
{
    public record GetRecentUsersQuery(int Days = 30) : IQuery<RecentUsersReportDto>;
}
