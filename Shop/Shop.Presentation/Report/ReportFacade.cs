using MediatR;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetList;
using Shop.Query.Products.GetBySlug;
using Shop.Query.ShopReport.FinancialReport;
using Shop.Query.ShopReport.FinancialReport.MonthlyIncome;
using Shop.Query.ShopReport.ProductReport.Dto;
using Shop.Query.ShopReport.ProductReport.GetAllProductReport;
using Shop.Query.ShopReport.UserReport.Dto;
using Shop.Query.ShopReport.UserReport.GetRecentUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Report
{
    public class ReportFacade : IReportFacade
    {
        private readonly IMediator _mediator;

        public ReportFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<ProductReportDto?>> GetAllProductReport()
        {
            return await _mediator.Send(new GetAllProductReportQuery());
        }

        public async Task<List<MonthlyIncomeReportDto?>> GetMonthlyIncomeReport(int MonthCount)
        {
            return await _mediator.Send(new GetMonthlyIncomeQuery(MonthCount));
        }

        public async Task<RecentUsersReportDto?> GetRecentUserReport(int Days)
        {
            return await _mediator.Send(new GetRecentUsersQuery(Days));
        }

        public async Task<IncomeReportDto?> GetTotalIncomeReport()
        {
            return await _mediator.Send(new GetTotalIncomeQuery());
        }
    }
}
