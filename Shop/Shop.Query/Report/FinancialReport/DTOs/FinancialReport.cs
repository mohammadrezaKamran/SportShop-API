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

public class MonthlyIncomeReportDto
{
    public string Month { get; set; }
    public decimal TotalIncome { get; set; }
}

public class IncomeReportDto
{
    public decimal TotalIncome { get; set; }
    public int TotalOrders { get; set; }
}
public class SalesReportDto
{
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
}