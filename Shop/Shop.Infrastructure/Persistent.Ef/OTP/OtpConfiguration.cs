using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Auth.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.OTP
{
    internal class OtpConfiguration : IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> builder)
        {
            builder.HasKey(o => o.Id); 

            builder.Property(o => o.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(o => o.Code)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(o => o.ExpireAt)
                   .IsRequired();

            builder.Property(o => o.IsUsed)
                   .IsRequired();
        }
    }
}
