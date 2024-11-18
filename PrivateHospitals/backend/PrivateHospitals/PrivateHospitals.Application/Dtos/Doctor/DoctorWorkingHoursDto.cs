using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Dtos.Doctor;

public record DoctorWorkingHoursDto
{
    public required string DoctorId { get; init; }
    public required Schedule Schedule { get; init; }
}