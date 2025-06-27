using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shop.Application.OTP;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.ChangePasswordByAdmin;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Application.Users.WishList.AddToWishList;
using Shop.Application.Users.WishList.RemoveWishList;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Products.DTOs;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.GetWishList;
using Shop.Query.Users.UserTokens;
using Shop.Query.Users.UserTokens.GetByJwtToken;
using Shop.Query.Users.UserTokens.GetByRefreshToken;

namespace Shop.Presentation.Facade.Users;

internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<UserFacade> _logger;

	public UserFacade(IMediator mediator, ILogger<UserFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}


	public async Task<OperationResult> CreateUser(CreateUserCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد کاربر");
			return OperationResult.Error("خطا در ایجاد کاربر");
		}
	}

	public async Task<OperationResult> AddToken(AddUserTokenCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزودن توکن");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
	{
		try
		{
			var result = await _mediator.Send(command);
			return result.Status == OperationResultStatus.Success
				? OperationResult.Success()
				: OperationResult.Error();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف توکن");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تغییر رمز عبور");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> ChangePasswordByAdmin(ChangeUserPasswordByAdminCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تغییر رمز عبور توسط ادمین");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> EditUser(EditUserCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش کاربر");
			return OperationResult.Error();
		}
	}

	public async Task<UserDto?> GetUserById(long userId)
	{
		try
		{
			return await _mediator.Send(new GetUserByIdQuery(userId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت کاربر با آیدی {UserId}", userId);
			return null;
		}
	}

	public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
	{
		try
		{
			return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت کاربر با شماره {Phone}", phoneNumber);
			return null;
		}
	}

	public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
	{
		try
		{
			var hash = Sha256Hasher.Hash(refreshToken);
			return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hash));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت Refresh Token");
			return null;
		}
	}

	public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
	{
		try
		{
			var hash = Sha256Hasher.Hash(jwtToken);
			return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hash));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت JWT Token");
			return null;
		}
	}

	public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
	{
		try
		{
			return await _mediator.Send(new GetUserByFilterQuery(filterParams));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در فیلتر کاربران");
			return new UserFilterResult();
		}
	}

	public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ثبت‌نام کاربر");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> AddToWishList(AddToWishListCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزودن به علاقه‌مندی‌ها");
			return OperationResult.Error();
		}
	}

	public async Task<OperationResult> RemoveWishList(RemoveWishListCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف از علاقه‌مندی‌ها");
			return OperationResult.Error();
		}
	}

	public async Task<List<WishListDto>> GetWishList(long userId)
	{
		try
		{
			return await _mediator.Send(new GetWishListQuery(userId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت علاقه‌مندی‌های کاربر {UserId}", userId);
			return new List<WishListDto>();
		}
	}

	public async Task<OperationResult> SendOTP(SendOtpCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ارسال OTP");
			return OperationResult.Error("خطا در ارسال کد تأیید");
		}
	}
}