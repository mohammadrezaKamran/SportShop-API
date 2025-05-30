using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.FinancialReport.MonthlyIncome
{
    public record GetMonthlyIncomeQuery(int MonthCount = 1) : IQuery<List<MonthlyIncomeReportDto>>;

}
