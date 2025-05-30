using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.UserReport.Dto
{
    public class RecentUsersReportDto
    {
        public int Count { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

public class LatestCommentDto:BaseDto
{
    public long ProductId { get; set; }
    public string Comment { get; set; }
}