using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovDigitalApp.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.RegistrationPlate).IsRequired().HasMaxLength(20);
        builder.Property(v => v.Make).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Model).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Colour).IsRequired().HasMaxLength(50);
        builder.Property(v => v.EngineDescription).IsRequired().HasMaxLength(100);
        builder.Property(v => v.FuelType).IsRequired().HasMaxLength(50);
        builder.Property(v => v.MotUntil).IsRequired().HasMaxLength(20);
        builder.HasOne(v => v.User)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
