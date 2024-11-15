using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Appointment;

public record CreateAppointmentDto
{
    public required DateOnly Date { get; init; }
    public required TimeSpan Time { get; init; }
    public required string PatientId { get; init; }
    public required string DoctorFirstName { get; init; }
    public required string DoctorLastName { get; init; }
}