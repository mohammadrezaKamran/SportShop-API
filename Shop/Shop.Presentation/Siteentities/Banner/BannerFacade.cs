using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Delete;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.Banners.GetList;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Banner;

internal class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<BannerFacade> _logger;
	public BannerFacade(IMediator mediator, ILogger<BannerFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}
	public async Task<OperationResult> CreateBanner(CreateBannerCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while creating banner");
			return OperationResult.Error("خطا در ایجاد بنر");
		}
	}

	public async Task<OperationResult> EditBanner(EditBannerCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while editing banner");
			return OperationResult.Error("خطا در ویرایش بنر");
		}
	}

	public async Task<OperationResult> DeleteBanner(long bannerId)
	{
		try
		{
			return await _mediator.Send(new DeleteBannerCommand(bannerId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while deleting banner with id {BannerId}", bannerId);
			return OperationResult.Error("خطا در حذف بنر");
		}
	}

	public async Task<BannerDto?> GetBannerById(long id)
	{
		try
		{
			return await _mediator.Send(new GetBannerByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while getting banner with id {Id}", id);
			return null;
		}
	}

	public async Task<List<BannerDto>> GetBanners()
	{
		try
		{
			return await _mediator.Send(new GetBannerListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while getting banner list");
			return new List<BannerDto>();
		}
	}


}