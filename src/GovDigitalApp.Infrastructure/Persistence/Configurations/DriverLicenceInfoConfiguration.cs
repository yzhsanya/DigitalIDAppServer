using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovDigitalApp.Infrastructure.Persistence.Configurations;

public class DriverLicenceInfoConfiguration : IEntityTypeConfiguration<DriverLicenceInfo>
{
    public void Configure(EntityTypeBuilder<DriverLicenceInfo> builder)
    {
        builder.ToTable("DriverLicenceInfos");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.LicenceNumber).IsRequired().HasMaxLength(50);
        builder.Property(d => d.TransmissionType).IsRequired().HasMaxLength(50);
        builder.Property(d => d.ValidFrom).IsRequired().HasMaxLength(20);
        builder.Property(d => d.ValidUntil).IsRequired().HasMaxLength(20);
        builder.HasOne(d => d.User)
            .WithOne(u => u.DriverLicenceInfo)
            .HasForeignKey<DriverLicenceInfo>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
