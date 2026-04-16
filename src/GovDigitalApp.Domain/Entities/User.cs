namespace GovDigitalApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<DocumentOrder> DocumentOrders { get; set; } = new List<DocumentOrder>();
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public DriverLicenceInfo? DriverLicenceInfo { get; set; }
}
