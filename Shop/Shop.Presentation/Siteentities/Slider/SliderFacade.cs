using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Delete;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Sliders.GetById;
using Shop.Query.SiteEntities.Sliders.GetList;

namespace Shop.Presentation.Facade.SiteEntities.Slider;

internal class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<SliderFacade> _logger;
	public SliderFacade(IMediator mediator, ILogger<SliderFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> CreateSlider(CreateSliderCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد اسلایدر");
			return OperationResult.Error("خطا در ایجاد اسلایدر");
		}
	}

	public async Task<OperationResult> EditSlider(EditSliderCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش اسلایدر");
			return OperationResult.Error("خطا در ویرایش اسلایدر");
		}
	}

	public async Task<OperationResult> DeleteSlider(long sliderId)
	{
		try
		{
			return await _mediator.Send(new DeleteSliderCommand(sliderId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف اسلایدر با آیدی {SliderId}", sliderId);
			return OperationResult.Error("خطا در حذف اسلایدر");
		}
	}

	public async Task<SliderDto?> GetSliderById(long id)
	{
		try
		{
			return await _mediator.Send(new GetSliderByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت اسلایدر با آیدی {Id}", id);
			return null;
		}
	}

	public async Task<List<SliderDto>> GetSliders()
	{
		try
		{
			return await _mediator.Send(new GetSliderListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست اسلایدرها");
			return new List<SliderDto>();
		}
	}
}