using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Shop.Domain.Auth.OTP;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IOtpRepository _otpRepository;

    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IOtpRepository otpRepository)
    {
        _repository = repository;
        _domainService = domainService;
        _otpRepository = otpRepository;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var otp = await _otpRepository.GetValidOtpAsync(request.PhoneNumber.Value, request.Code);
        if (otp == null || !otp.IsValid(request.Code))
            throw new Exception("کد نامعتبر یا منقضی شده است");

        otp.MarkAsUsed();    

        var user = User.RegisterUser(request.PhoneNumber.Value, Sha256Hasher.Hash(request.Password), _domainService);
        user.VerifyPhoneNumber();

        _repository.Add(user);
        await _otpRepository.Save();
        await _repository.Save();

        return OperationResult.Success();
    }
}
