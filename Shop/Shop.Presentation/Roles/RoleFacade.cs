using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Roles.DTOs;
using Shop.Query.Roles.GetById;
using Shop.Query.Roles.GetList;

namespace Shop.Presentation.Facade.Roles;

internal class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<RoleFacade> _logger;

	public RoleFacade(IMediator mediator, ILogger<RoleFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> CreateRole(CreateRoleCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد نقش");
			return OperationResult.Error("در ایجاد نقش مشکلی پیش آمد");
		}
	}

	public async Task<OperationResult> EditRole(EditRoleCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش نقش");
			return OperationResult.Error("در ویرایش نقش مشکلی پیش آمد");
		}
	}

	public async Task<RoleDto?> GetRoleById(long roleId)
	{
		try
		{
			return await _mediator.Send(new GetRoleByIdQuery(roleId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت نقش با شناسه {RoleId}", roleId);
			return null;
		}
	}

	public async Task<List<RoleDto>> GetRoles()
	{
		try
		{
			return await _mediator.Send(new GetRoleListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست نقش‌ها");
			return new List<RoleDto>();
		}
	}
}