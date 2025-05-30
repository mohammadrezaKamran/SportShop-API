using MediatR;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetList;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Report.FinancialReport.MonthlyIncome;
using Shop.Query.Report.FinancialReport.TodaysSales;
using Shop.Query.Report.FinancialReport.TotalIncome;
using Shop.Query.Report.Order.GetNewOrder;
using Shop.Query.Report.ProductReport.Dto;
using Shop.Query.Report.ProductReport.GetBestSellersProduct;
using Shop.Query.Report.ProductReport.GetCountOfProducts;
using Shop.Query.Report.ProductReport.GetOutofStuckProduct;
using Shop.Query.Report.UserReport.Dto;
using Shop.Query.Report.UserReport.GetLatestComment;
using Shop.Query.Report.UserReport.GetRecentUsers;
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

        public async Task<List<ProductSalesDto>> GetBestSellersProduct()
        {
            return await _mediator.Send(new GetBestSellersQuery());
        }

        public async Task<long> GetCountOFProduct()
        {
            return await _mediator.Send(new GetProductCountQuery());
        }

        public async Task<List<LatestCommentDto>> GetLatestComment()
        {
            return await _mediator.Send(new GetLatestCommentQuery());
        }

        public async Task<List<MonthlyIncomeReportDto?>> GetMonthlyIncomeReport(int MonthCount)
        {
            return await _mediator.Send(new GetMonthlyIncomeQuery(MonthCount));
        }

        public async Task<long> GetNewOrder()
        {
            return await _mediator.Send(new GetNewOrdersQuery());
        }

        public async Task<List<OutOfStockProductDto>> GetOutOfStockProduct()
        {
            return await _mediator.Send(new GetOutOfStockProductsQuery());
        }

        public async Task<RecentUsersReportDto?> GetRecentUserReport(int Days)
        {
            return await _mediator.Send(new GetRecentUsersQuery(Days));
        }

        public async Task<decimal> GetTodaysSales()
        {
            return await _mediator.Send(new GetTodaysSalesQuery());
        }

        public async Task<IncomeReportDto?> GetTotalIncomeReport()
        {
            return await _mediator.Send(new GetTotalIncomeQuery());
        }
    }
}
