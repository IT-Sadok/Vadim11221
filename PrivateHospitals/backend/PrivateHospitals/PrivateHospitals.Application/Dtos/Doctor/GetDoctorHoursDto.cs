using Microsoft.AspNetCore.Builder;
using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Doctor;

public record GetDoctorHoursDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DoctorSpecialities Speciality { get; init; }
    public List<Core.Models.WorkingHours> WorkingHours { get; init; }
}