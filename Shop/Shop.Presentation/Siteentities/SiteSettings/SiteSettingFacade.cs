using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.SiteEntities.SiteSettings.Create;
using Shop.Application.SiteEntities.SiteSettings.Edit;
using Shop.Presentation.Facade.Siteentities.SiteSettings;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetAll;
using Shop.Query.SiteEntities.SiteSettings.GetByGroup;
using Shop.Query.SiteEntities.SiteSettings.GetById;
using Shop.Query.SiteEntities.SiteSettings.GetByKey;
using Shop.Query.SiteEntities.SiteSettings.GetSeoDataForPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class SiteSettingFacade : ISiteSettingFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<SiteSettingFacade> _logger;
	public SiteSettingFacade(IMediator mediator, ILogger<SiteSettingFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> CreateSiteSetting(CreateSiteSettingCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد SiteSetting");
			return OperationResult.Error("خطا در ایجاد SiteSetting");
		}
	}

	public async Task<OperationResult> EditSiteSetting(EditSiteSettingCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش SiteSetting");
			return OperationResult.Error("خطا در ویرایش SiteSetting");
		}
	}

	public async Task<SeoDataDto?> GetSeoDataForPage(string pageKey)
	{
		try
		{
			return await _mediator.Send(new GetSeoDataForPagesQuery(pageKey));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت SeoData برای صفحه {PageKey}", pageKey);
			return null;
		}
	}

	public async Task<SiteSettingDto?> GetSiteSettingById(long id)
	{
		try
		{
			return await _mediator.Send(new GetSiteSettingByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت SiteSetting با آیدی {Id}", id);
			return null;
		}
	}

	public async Task<SiteSettingDto?> GetSiteSettingByKey(string key)
	{
		try
		{
			return await _mediator.Send(new GetSiteSettingByKeyQuery(key));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت SiteSetting با کلید {Key}", key);
			return null;
		}
	}

	public async Task<List<SiteSettingDto>> GetSiteSettings()
	{
		try
		{
			return await _mediator.Send(new GetSiteSettingListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست SiteSettingها");
			return new List<SiteSettingDto>();
		}
	}

	public async Task<List<SiteSettingDto>> GetSiteSettingsByGroup(SiteSettingGroup group)
	{
		try
		{
			return await _mediator.Send(new GetSiteSettingByQuery(group));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت SiteSettingها بر اساس گروه {Group}", group);
			return new List<SiteSettingDto>();
		}
	}
}
