using GovDigitalApp.Application.Common;
using GovDigitalApp.Domain.Entities;
using GovDigitalApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentOrder> DocumentOrders => Set<DocumentOrder>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<DriverLicenceInfo> DriverLicenceInfos => Set<DriverLicenceInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
