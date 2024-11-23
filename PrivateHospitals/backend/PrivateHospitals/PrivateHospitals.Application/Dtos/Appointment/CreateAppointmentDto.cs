using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Appointment;

public record CreateAppointmentDto
{
    public required DateTime Date { get; init; }
    public required string DoctorId { get; init; }
}