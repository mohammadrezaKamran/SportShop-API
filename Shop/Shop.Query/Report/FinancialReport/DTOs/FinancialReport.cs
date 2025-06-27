using MediatR;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.FinancialReport.DTOs
{
    internal class FinancialReport
    {

    }
}

public class IncomeReportDto
{
    public decimal TotalIncome { get; set; }
    public decimal SixMonthlyIncome { get; set; }
    public decimal MonthlyIncome { get; set; }
    public decimal DailyIncome { get; set; }
}