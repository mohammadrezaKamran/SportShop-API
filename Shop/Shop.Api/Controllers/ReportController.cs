using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Report;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Comments.DTOs;
using Shop.Query.Roles.DTOs;
using Shop.Query.ShopReport.ProductReport.Dto;
using Shop.Query.ShopReport.UserReport.Dto;

namespace Shop.Api.Controllers;

public class ReportController : ApiController
{
    private readonly IReportFacade _reportFacade;

    public ReportController(IReportFacade reportFacade)
    {
        _reportFacade = reportFacade;
    }

    [PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("RecentUsers/{Days}")]
    public async Task<ApiResult<RecentUsersReportDto?>> GetRecentUserReport(int Days)
    {
        var result = await _reportFacade.GetRecentUserReport(Days);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("Products")]
    public async Task<ApiResult<List<ProductReportDto?>>> GetAllProductReport()
    {
        var result = await _reportFacade.GetAllProductReport();
        return QueryResult(result);
    }

    [PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("MonthlyIncome/{MonthCount}")]
    public async Task<ApiResult<List<MonthlyIncomeReportDto?>>> GetMonthlyIncomeReport(int MonthCount)
    {
        var result = await _reportFacade.GetMonthlyIncomeReport(MonthCount);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("TotalIncome")]
    public async Task<ApiResult<IncomeReportDto?>> GetTotalIncomeReport()
    {
        var result = await _reportFacade.GetTotalIncomeReport();
        return QueryResult(result);
    }

}