using Common.Application;
using Common.Application.Validation;
using MediatR;
using Shop.Application.OTP;
using Shop.Domain.Auth.OTP;

public class SendOtpCommandHandler : IBaseCommandHandler<SendOtpCommand>
{
    private readonly IOtpRepository _otpRepo;
    private readonly ISmsSender _smsSender;

    public SendOtpCommandHandler(IOtpRepository otpRepo, ISmsSender smsSender)
    {
        _otpRepo = otpRepo;
        _smsSender = smsSender;
    }

    public async Task<OperationResult> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var code = new Random().Next(1000, 9999).ToString();
        var otp = new Otp(request.PhoneNumber, code, TimeSpan.FromMinutes(5));
        await _otpRepo.AddAsync(otp);
        await _smsSender.SendSmsAsync(request.PhoneNumber, $"کد تایید شما: {code}");
        await _otpRepo.Save();
        return OperationResult.Success();
    }
}
