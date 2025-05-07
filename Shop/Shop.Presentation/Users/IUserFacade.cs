using Common.Application;
using Shop.Application.OTP;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Application.Users.WishList.AddToWishList;
using Shop.Application.Users.WishList.RemoveWishList;
using Shop.Domain.UserAgg;
using Shop.Query.Products.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users
{
    public interface IUserFacade
    {
        Task<OperationResult> RegisterUser(RegisterUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult> AddToken(AddUserTokenCommand command);
        Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);
        Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
        Task<OperationResult> AddToWishList(AddToWishListCommand command);
        Task<OperationResult> RemoveWishList(RemoveWishListCommand command);
        Task<OperationResult> SendOTP(SendOtpCommand command);

        Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
        Task<UserDto?> GetUserById(long userId);
        Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken);
        Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);
        Task<List<ProductDto?>> GetWishList(long userId);
        Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
    }
}