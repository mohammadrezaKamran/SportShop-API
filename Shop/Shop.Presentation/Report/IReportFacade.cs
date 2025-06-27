using Common.Application;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Delete;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;
using Shop.Query.Report.Order.Dtos;
using Shop.Query.Report.ProductReport.Dto;
using Shop.Query.Report.UserReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Report
{
    public interface IReportFacade
    {
        Task<IncomeReportDto?> GetTotalIncomeReport();

        Task<List<ProductSalesDto>> GetBestSellersProduct();
        Task<List<OutOfStockProductDto>> GetOutOfStockProduct();

        Task<RecentUsersReportDto?> GetRecentUserReport(int Days);
        Task<List<LatestCommentDto>> GetLatestComment();

        Task<List<RecentOrderDto>> GetRecentOrder();
        Task<NumberOfThingDto> GetNumberOfThings();
    }
}
 