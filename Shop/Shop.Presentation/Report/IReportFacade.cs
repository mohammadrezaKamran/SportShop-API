using Common.Application;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Delete;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;
using Shop.Query.ShopReport.ProductReport.Dto;
using Shop.Query.ShopReport.UserReport.Dto;
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
        Task<List<MonthlyIncomeReportDto?>> GetMonthlyIncomeReport(int MonthCount);

        Task<List<ProductReportDto?>> GetAllProductReport();
        Task<RecentUsersReportDto?> GetRecentUserReport(int Days);

    }
}
 