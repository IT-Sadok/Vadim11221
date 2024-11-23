using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Doctor;

public record DoctorDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DoctorSpecialities Speciality { get; init; } 
}