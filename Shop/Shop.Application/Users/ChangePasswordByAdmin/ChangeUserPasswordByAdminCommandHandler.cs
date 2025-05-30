using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Application.Users.ChangePasswordByAdmin;
using Shop.Domain.UserAgg.Repository;

public class ChangeUserPasswordByAdminCommandHandler : IBaseCommandHandler<ChangeUserPasswordByAdminCommand>
{
	private readonly IUserRepository _userRepository;

	public ChangeUserPasswordByAdminCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResult> Handle(ChangeUserPasswordByAdminCommand request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetTracking(request.UserId);
		if (user == null)
			return OperationResult.NotFound("کاربر یافت نشد");

		var newPasswordHash = Sha256Hasher.Hash(request.NewPassword);
		user.ChangePassword(newPasswordHash);
		await _userRepository.Save();

		return OperationResult.Success();
	}
}
