using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Report;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Comments.DTOs;
using Shop.Query.Report.Order.Dtos;
using Shop.Query.Report.ProductReport.Dto;
using Shop.Query.Report.UserReport.Dto;
using Shop.Query.Roles.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.PanelAdmin)]
public class ReportController : ApiController
{
    private readonly IReportFacade _reportFacade;

    public ReportController(IReportFacade reportFacade)
    {
        _reportFacade = reportFacade;
    }

    //[PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("TotalIncome")]
    public async Task<ApiResult<IncomeReportDto?>> GetTotalIncomeReport()
    {
        var result = await _reportFacade.GetTotalIncomeReport();
        return QueryResult(result);
    }

    //[PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("BestSellersProduct")]
    public async Task<ApiResult<List<ProductSalesDto>>> GetBestSellersProduct()
    {
        var result = await _reportFacade.GetBestSellersProduct();
        return QueryResult(result);
    }

    //[PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("OutOfStockProduct")]
    public async Task<ApiResult<List<OutOfStockProductDto>>> GetOutOfStockProduct()
    {
        var result = await _reportFacade.GetOutOfStockProduct();
        return QueryResult(result);
    }

    //[PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("RecentUsers/{Days}")]
    public async Task<ApiResult<RecentUsersReportDto?>> GetRecentUserReport(int Days)
    {
        var result = await _reportFacade.GetRecentUserReport(Days);
        return QueryResult(result);
    }

    //[PermissionChecker(Permission.PanelAdmin)]
    [HttpGet("LatestComment")]
    public async Task<ApiResult<List<LatestCommentDto>>> GetLatestComment()
    {
        var result = await _reportFacade.GetLatestComment();
        return QueryResult(result);
    }

    [HttpGet("RecentOrder")]
    public async Task<ApiResult<List<RecentOrderDto>>> GetRecentOrder()
    {
        var result = await _reportFacade.GetRecentOrder();
        return QueryResult(result);
    }

    [HttpGet("NumberOfThings")]
    public async Task<ApiResult<NumberOfThingDto>> GetNumberOfThings()
    {
        var result = await _reportFacade.GetNumberOfThings();
        return QueryResult(result);
    }
}