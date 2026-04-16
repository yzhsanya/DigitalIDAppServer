using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovDigitalApp.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        builder.HasKey(d => d.Id);
        builder.HasDiscriminator(d => d.DocumentType)
            .HasValue<PassportDocument>(DocumentType.Passport)
            .HasValue<DrivingLicenceDocument>(DocumentType.DrivingLicence)
            .HasValue<TaxCodeDocument>(DocumentType.NationalInsurance)
            .HasValue<DiplomaDocument>(DocumentType.Diploma)
            .HasValue<BirthCertificateDocument>(DocumentType.BirthCertificate)
            .HasValue<EVisaDocument>(DocumentType.EVisa)
            .HasValue<PassportToTravelAbroadDocument>(DocumentType.PassportToTravelAbroad)
            .HasValue<ResidentPermitDocument>(DocumentType.ResidentPermit)
            .HasValue<MarriageCertificateDocument>(DocumentType.MarriageCertificate);
        builder.Property(d => d.SortKey).IsRequired();
        builder.Property(d => d.CreatedAt).IsRequired();
        builder.HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
