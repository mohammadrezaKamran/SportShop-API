using Common.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.FinancialReport.TodaysSales
{
    public class GetTodaysSalesQuery:IRequest<decimal>
    {
    }
}
