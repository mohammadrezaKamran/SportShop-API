using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;
internal class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
	public void Configure(EntityTypeBuilder<Banner> builder)
	{
		builder.ToTable("Banners", "banner");

		builder.Property(b => b.ImageName)
			.HasMaxLength(120).IsRequired();

		builder.Property(b => b.Link)
			.HasMaxLength(500).IsRequired();

		builder.Property(b => b.Title)
			.HasMaxLength(500)
			.IsRequired(false); 

		builder.Property(b => b.Description)
			.HasMaxLength(500)
			.IsRequired(false); 

		builder.Property(b => b.IsActive)
			.IsRequired();

		builder.Property(b => b.Order)
			.IsRequired().IsUnicode();

		builder.Property(b => b.AltText)
			.HasMaxLength(255)
			.IsRequired();
	}
}