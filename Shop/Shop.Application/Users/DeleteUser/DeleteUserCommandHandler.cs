using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Application.SiteEntities.Banners.Delete;
using Shop.Application.Users.DeleteUser;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.UserAgg.Repository;

internal class DeleteUserCommandHandler : IBaseCommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.id);
        if(user == null)
            return OperationResult.NotFound();
        //_userRepository.Delete(user);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}
