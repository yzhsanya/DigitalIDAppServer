using GovDigitalApp.Application.Common;
using GovDigitalApp.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IDataProtector? _protector;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public AppDbContext(DbContextOptions<AppDbContext> options, IDataProtectionProvider? protectionProvider)
        : base(options)
    {
        _protector = protectionProvider?.CreateProtector("GovDigitalApp.Pii.v1");
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentOrder> DocumentOrders => Set<DocumentOrder>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<DriverLicenceInfo> DriverLicenceInfos => Set<DriverLicenceInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        if (_protector != null)
        {
            var converter = new EncryptedStringConverter(_protector);
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasConversion(converter);
            modelBuilder.Entity<User>().Property(u => u.LastName).HasConversion(converter);
        }
    }
}
