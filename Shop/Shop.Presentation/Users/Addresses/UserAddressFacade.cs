using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Users.Addresses.GetById;
using Shop.Query.Users.Addresses.GetList;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses
{
    internal class UserAddressFacade : IUserAddressFacade
    {
        private readonly IMediator _mediator;
		private readonly ILogger<UserAddressFacade> _logger;
		public UserAddressFacade(IMediator mediator, ILogger<UserAddressFacade> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		public async Task<OperationResult> AddAddress(AddUserAddressCommand command)
		{
			try
			{
				return await _mediator.Send(command);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در افزودن آدرس کاربر");
				return OperationResult.Error("خطایی در افزودن آدرس رخ داده است");
			}
		}

		public async Task<OperationResult> EditAddress(EditUserAddressCommand command)
		{
			try
			{
				return await _mediator.Send(command);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ویرایش آدرس کاربر");
				return OperationResult.Error("خطایی در ویرایش آدرس رخ داده است");
			}
		}

		public async Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command)
		{
			try
			{
				return await _mediator.Send(command);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف آدرس کاربر");
				return OperationResult.Error("خطایی در حذف آدرس رخ داده است");
			}
		}

		public async Task<AddressDto?> GetById(long userAddressId)
		{
			try
			{
				return await _mediator.Send(new GetUserAddressByIdQuery(userAddressId));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت آدرس با آیدی {AddressId}", userAddressId);
				return null;
			}
		}

		public async Task<AddressDto> GetUserAddress(long userId)
		{
			try
			{
				return await _mediator.Send(new GetUserAddressQuery(userId));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت آدرس کاربر با آیدی {UserId}", userId);
				return new AddressDto(); // برمی‌گردونه آدرس خالی برای جلوگیری از کرش
			}
		}

	}
}