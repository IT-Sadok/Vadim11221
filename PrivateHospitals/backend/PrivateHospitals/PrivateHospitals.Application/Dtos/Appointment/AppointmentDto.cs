namespace PrivateHospitals.Application.Dtos.Appointment;

public record AppointmentDto
{
    public required DateTime AppointmentDate { get; init; }
    
}