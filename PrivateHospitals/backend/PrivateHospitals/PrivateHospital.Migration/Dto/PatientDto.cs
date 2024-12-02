namespace PrivateHospital.Migration.Dto;

public record PatientDto
{
    public string ExternalId { get; init; }
    public string CompanyId { get; set; }
    public string PatientId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; } = "";
}