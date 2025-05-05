using Microsoft.EntityFrameworkCore;
using Shop.Domain.Auth.OTP;
using Shop.Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.OTP
{
    internal class OtpRepository : BaseRepository<Otp>, IOtpRepository
    {
        public OtpRepository(ShopContext context) : base(context)
        {

        }

        public async Task<Otp?> GetValidOtpAsync(string phoneNumber, string code)
        {
            return await Context.OTP
        .Where(o => o.PhoneNumber == phoneNumber && o.Code == code && !o.IsUsed && o.ExpireAt >= DateTime.UtcNow)
        .OrderByDescending(o => o.ExpireAt)
        .FirstOrDefaultAsync();
        }
    }
}
