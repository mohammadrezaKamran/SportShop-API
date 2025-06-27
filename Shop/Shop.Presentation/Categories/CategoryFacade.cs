using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;

namespace Shop.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<CategoryFacade> _logger;
	public CategoryFacade(IMediator mediator, ILogger<CategoryFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult<long>> AddChild(AddChildCategoryCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در AddChild: {@Command}", command);
			return OperationResult<long>.Error("خطایی در افزودن زیرشاخه رخ داد.");
		}
	}

	public async Task<OperationResult> Edit(EditCategoryCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در Edit: {@Command}", command);
			return OperationResult.Error("خطایی در ویرایش دسته‌بندی رخ داد.");
		}
	}

	public async Task<OperationResult<long>> Create(CreateCategoryCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در Create: {@Command}", command);
			return OperationResult<long>.Error("خطایی در ایجاد دسته‌بندی رخ داد.");
		}
	}

	public async Task<OperationResult> Remove(long categoryId)
	{
		try
		{
			return await _mediator.Send(new RemoveCategoryCommand(categoryId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در Remove: CategoryId={CategoryId}", categoryId);
			return OperationResult.Error("خطایی در حذف دسته‌بندی رخ داد.");
		}
	}

	public async Task<CategoryDto?> GetCategoryById(long id)
	{
		try
		{
			return await _mediator.Send(new GetCategoryByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در GetCategoryById: Id={Id}", id);
			return null;
		}
	}

	public async Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId)
	{
		try
		{
			return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در GetCategoriesByParentId: ParentId={ParentId}", parentId);
			return new List<ChildCategoryDto>();
		}
	}

	public async Task<List<CategoryDto>> GetCategories()
	{
		try
		{
			return await _mediator.Send(new GetCategoryListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در GetCategories");
			return new List<CategoryDto>();
		}
	}
}