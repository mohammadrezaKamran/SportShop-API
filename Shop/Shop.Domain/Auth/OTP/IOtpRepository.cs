using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Auth.OTP
{
    public interface IOtpRepository:IBaseRepository<Otp>
    {
        Task<Otp?> GetValidOtpAsync(string phoneNumber, string code);
    }
}
