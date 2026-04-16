namespace GovDigitalApp.Domain.Entities;

public class DocumentOrder
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
    public int Position { get; set; }
}
