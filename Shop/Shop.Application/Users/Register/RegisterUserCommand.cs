using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.Register;

public class RegisterUserCommand : IBaseCommand
{
    public RegisterUserCommand(PhoneNumber phoneNumber, string password/*, string code*/)
    {
        PhoneNumber = phoneNumber;
        Password = password;
        //Code = code;
    }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Password { get; private set; }
    //public string Code { get; private set; }
}