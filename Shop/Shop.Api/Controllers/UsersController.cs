﻿using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Users;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.ChangePasswordByAdmin;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.WishList.AddToWishList;
using Shop.Application.Users.WishList.RemoveWishList;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Products.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;


[Authorize]
public class UsersController : ApiController
{
	private readonly IUserFacade _userFacade;
	private readonly IMapper _mapper;
	public UsersController(IUserFacade userFacade, IMapper mapper)
	{
		_userFacade = userFacade;
		_mapper = mapper;
	}
	[PermissionChecker(Permission.User_Management)]
	[HttpGet]
	public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
	{
		var result = await _userFacade.GetUserByFilter(filterParams);
		return QueryResult(result);
	}
	[HttpGet("Current")]
	public async Task<ApiResult<UserDto>> GetCurrentUser()
	{
		var result = await _userFacade.GetUserById(User.GetUserId());
		return QueryResult(result);
	}

	[PermissionChecker(Permission.User_Management)]
	[HttpGet("{userId}")]
	public async Task<ApiResult<UserDto?>> GetById(long userId)
	{
		var result = await _userFacade.GetUserById(userId);
		return QueryResult(result);
	}

	[PermissionChecker(Permission.User_Management)]
	[HttpPost]
	public async Task<ApiResult> Create(CreateUserCommand command)
	{
		var result = await _userFacade.CreateUser(command);
		return CommandResult(result);
	}

	[HttpPut("ChangePassword")]
	public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
	{
		var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
		changePasswordModel.UserId = User.GetUserId();
		var result = await _userFacade.ChangePassword(changePasswordModel);
		return CommandResult(result);
	}

	//[PermissionChecker(Permission.PanelAdmin)]
	[HttpPut("ChangePasswordByAdmin")]
	public async Task<ApiResult> ChangePasswordByAdmin(ChangePasswordByAdminViewModel command)
	{

		var result = await _userFacade.ChangePasswordByAdmin(new ChangeUserPasswordByAdminCommand()
		{
			UserId = command.UserId,
			NewPassword = command.NewPassword
		});
		return CommandResult(result);
	}

	[HttpPut("Current")]
	public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
	{
		var commandModel = new EditUserCommand(User.GetUserId(), command.Avatar, command.Name, command.Family,
			command.PhoneNumber, command.Email, command.Gender);

		var result = await _userFacade.EditUser(commandModel);
		return CommandResult(result);
	}

	[PermissionChecker(Permission.User_Management)]
	[HttpPut]
	public async Task<ApiResult> Edit([FromForm] EditUserWithIdViewModel command)
	{
		var commandModel = new EditUserCommand(long.Parse(command.UserId), command.Avatar, command.Name, command.Family,
			 command.PhoneNumber, command.Email, command.Gender);

		var result = await _userFacade.EditUser(commandModel);
		return CommandResult(result);
	}

	[HttpPost("WishList")]
	public async Task<ApiResult> AddToWishList(AddToWishListCommand command)
	{
		var result = await _userFacade.AddToWishList(command);
		return CommandResult(result);
	}

	[HttpDelete("WishList/{productId}")]
	public async Task<ApiResult> RemoveWishList(long productId)
	{
		var result = await _userFacade.RemoveWishList(new RemoveWishListCommand()
		{
			ProductId = productId,
			UserId = User.GetUserId()
		});
		return CommandResult(result);
	}

	[HttpGet("WishList")]
	public async Task<ApiResult<List<WishListDto?>>> GetWishList()
	{
		var result = await _userFacade.GetWishList(User.GetUserId());
		return QueryResult<List<WishListDto?>>(result);
	}
}