using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Appointment;

public record CreateAppointmentDto
{
    public DateTime Date { get; init; }
    public string PatientId { get; init; }
    public string DoctorFirstName { get; init; }
    public string DoctorLastName { get; init; }
}