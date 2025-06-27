using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Report;
using Shop.Query.Report.FinancialReport.TotalIncome;
using Shop.Query.Report.Order.Dtos;
using Shop.Query.Report.Order.GetNewOrder;
using Shop.Query.Report.ProductReport.Dto;
using Shop.Query.Report.ProductReport.GetBestSellersProduct;
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
		private readonly ILogger<ReportFacade> _logger;

		public ReportFacade(IMediator mediator, ILogger<ReportFacade> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		public async Task<List<ProductSalesDto>> GetBestSellersProduct()
		{
			try
			{
				return await _mediator.Send(new GetBestSellersQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت پرفروش‌ترین محصولات");
				return new List<ProductSalesDto>();
			}
		}

		public async Task<List<LatestCommentDto>> GetLatestComment()
		{
			try
			{
				return await _mediator.Send(new GetLatestCommentQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت آخرین نظرات");
				return new List<LatestCommentDto>();
			}
		}

		public async Task<NumberOfThingDto> GetNumberOfThings()
		{
			try
			{
				return await _mediator.Send(new GetNumberOfThingsQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت تعداد آیتم‌ها");
				return new NumberOfThingDto();
			}
		}

		public async Task<List<OutOfStockProductDto>> GetOutOfStockProduct()
		{
			try
			{
				return await _mediator.Send(new GetOutOfStockProductsQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت محصولات ناموجود");
				return new List<OutOfStockProductDto>();
			}
		}

		public async Task<List<RecentOrderDto>> GetRecentOrder()
		{
			try
			{
				return await _mediator.Send(new GetRecentOrdersQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت سفارش‌های اخیر");
				return new List<RecentOrderDto>();
			}
		}

		public async Task<RecentUsersReportDto?> GetRecentUserReport(int days)
		{
			try
			{
				return await _mediator.Send(new GetRecentUsersQuery(days));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت گزارش کاربران جدید برای {Days} روز اخیر", days);
				return null;
			}
		}

		public async Task<IncomeReportDto?> GetTotalIncomeReport()
		{
			try
			{
				return await _mediator.Send(new GetTotalIncomeQuery());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت گزارش درآمد کلی");
				return null;
			}
		}
	}
}
