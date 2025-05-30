﻿using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
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
    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task<OperationResult> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Status != OperationResultStatus.Success)
            return OperationResult.Error();

        return OperationResult.Success();
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        
            return await _mediator.Send(new GetUserByIdQuery(userId));
       
    }

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
    }

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        var hashJwtToken = Sha256Hasher.Hash(jwtToken);
      
            return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
     
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToWishList(AddToWishListCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveWishList(RemoveWishListCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<List<WishListDto>> GetWishList(long userId)
    {
        return await _mediator.Send(new GetWishListQuery(userId));
    }

    public async Task<OperationResult> SendOTP(SendOtpCommand command)
    {
        return await _mediator.Send(command);
    }

	public async Task<OperationResult> ChangePasswordByAdmin(ChangeUserPasswordByAdminCommand command)
	{
		return await _mediator.Send(command);
	}
}