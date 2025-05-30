using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities
{
    public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
    {
        public void Configure(EntityTypeBuilder<SiteSetting> builder)
        {
            builder.ToTable("SiteSettings", "siteSettings");

            builder.HasIndex(e => e.Key).IsUnique();

            builder.Property(b => b.Key)
                .HasMaxLength(100).IsRequired();

            builder.Property(b => b.Value).IsRequired();

            builder.Property(b => b.Description)
             .HasMaxLength(500).IsRequired();
        }
    }
}
