using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Auth.OTP
{
    public class Otp:BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public string Code { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public bool IsUsed { get; private set; }

        public Otp(string phoneNumber, string code, TimeSpan validFor)
        {
            PhoneNumber = phoneNumber;
            Code = code;
            ExpireAt = DateTime.UtcNow.Add(validFor);
            IsUsed = false;
        }
        private Otp() { }

        public bool IsValid(string inputCode)
        {
            return !IsUsed && DateTime.UtcNow <= ExpireAt && Code == inputCode;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }
    }
}
