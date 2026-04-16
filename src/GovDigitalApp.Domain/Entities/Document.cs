namespace GovDigitalApp.Domain.Entities;

public abstract class Document
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
    public long SortKey { get; set; }
    public DateTime CreatedAt { get; set; }
}
