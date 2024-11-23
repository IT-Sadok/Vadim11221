namespace PrivateHospitals.Application.Dtos.Patient;

public record PatientDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}