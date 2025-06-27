using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;

internal class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
	public void Configure(EntityTypeBuilder<Slider> builder)
	{
		builder.ToTable("Sliders", "slider");

		builder.Property(b => b.ImageName)
			.HasMaxLength(120).IsRequired();

		builder.Property(s => s.Title)
	.HasMaxLength(500)
	.IsRequired();

		builder.Property(s => s.Link)
			.HasMaxLength(500)
			.IsRequired();


		builder.Property(s => s.Description)
			.HasMaxLength(500)
			.IsRequired(false); // چون nullable هست

		builder.Property(s => s.IsActive)
			.IsRequired();

		builder.Property(s => s.Order)
			.IsRequired();

		builder.Property(s => s.AltText)
			.HasMaxLength(255)
			.IsRequired();
	}
}