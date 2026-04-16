namespace GovDigitalApp.Domain.Entities;

public class NhsCardDocument : Document
{
    public string NhsNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string RegisteredGpSurgery { get; set; } = string.Empty;
}
