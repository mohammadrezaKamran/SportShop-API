using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Delete;
using Shop.Application.Comments.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;

namespace Shop.Presentation.Facade.Comments;

internal class CommentFacade: ICommentFacade
{
    private  readonly IMediator _mediator;
	private readonly ILogger<CommentFacade> _logger;
	public CommentFacade(IMediator mediator, ILogger<CommentFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تغییر وضعیت کامنت: {@Command}", command);
			return OperationResult.Error("خطایی در تغییر وضعیت کامنت رخ داد.");
		}
	}

	public async Task<OperationResult> CreateComment(CreateCommentCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد کامنت: {@Command}", command);
			return OperationResult.Error("خطایی در ثبت کامنت رخ داد.");
		}
	}

	public async Task<OperationResult> EditComment(EditCommentCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش کامنت: {@Command}", command);
			return OperationResult.Error("خطایی در ویرایش کامنت رخ داد.");
		}
	}

	public async Task<OperationResult> DeleteComment(DeleteCommentCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف کامنت: {@Command}", command);
			return OperationResult.Error("خطایی در حذف کامنت رخ داد.");
		}
	}

	public async Task<CommentDto?> GetCommentById(long id)
	{
		try
		{
			return await _mediator.Send(new GetCommentByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت کامنت با شناسه: {Id}", id);
			return null;
		}
	}

	public async Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams)
	{
		try
		{
			return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست کامنت‌ها با فیلتر: {@Filter}", filterParams);
			return new CommentFilterResult(); // یا مقدار پیش‌فرض مناسب
		}
	}
}