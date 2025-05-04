using Shop.Infrastructure._Utilities;
using Shop.Domain.SupportTicketAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.Ef.SupportTicket
{
    internal class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicketAgg>
    {
        public void Configure(EntityTypeBuilder<SupportTicketAgg> builder)
        {
            builder.ToTable("SupportTicket", "supportTicket");

            builder.HasIndex(b => b.UserId);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(t => t.Status)
                .HasConversion<int>()
                .IsRequired();


            builder.OwnsMany(b => b.Replies, option =>
            {
                option.ToTable("TicketReplies", "SupportTicket");

                option.HasIndex(b => b.AdminId);

                option.Property(b => b.Message)
                    .IsRequired(false)
                    .HasMaxLength(500);
            });
        }
    }
}
