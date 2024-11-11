using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Doctor;

public record RegisterDoctorDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required SpecialitiesOfDoctor Speciality { get; init; }
    public required string Password { get; init; }
}
