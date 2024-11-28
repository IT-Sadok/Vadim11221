using PrivateHospitals.Core.Enum;

namespace PrivateHospital.Migration.Dto;

public record DoctorDto
{
    public string ExternalId { get; init; }
    public string UserName { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; } = "";
    public DoctorSpecialities Speciality { get; init; }
}