using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Application.Dtos.Appointment;

public record AppointmentDto
{
    public required DateTime Date { get; init; }
    public required AppointmentStatuses Status { get; init; }
    public DoctorDto Doctor { get; init; }
    public PatientDto Patient { get; init; }
}