using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Application.Common;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Document> Documents { get; }
    DbSet<DocumentOrder> DocumentOrders { get; }
    DbSet<Vehicle> Vehicles { get; }
    DbSet<DriverLicenceInfo> DriverLicenceInfos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
