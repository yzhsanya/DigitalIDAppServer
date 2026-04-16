using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovDigitalApp.Infrastructure.Persistence.Configurations;

public class DocumentOrderConfiguration : IEntityTypeConfiguration<DocumentOrder>
{
    public void Configure(EntityTypeBuilder<DocumentOrder> builder)
    {
        builder.ToTable("DocumentOrders");
        builder.HasKey(o => o.Id);
        builder.HasOne(o => o.User)
            .WithMany(u => u.DocumentOrders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(o => o.Document)
            .WithMany()
            .HasForeignKey(o => o.DocumentId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(o => new { o.UserId, o.DocumentId }).IsUnique();
    }
}
