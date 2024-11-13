using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Responses;

namespace PrivateHospitals.Application.Interfaces.Appointment;

public interface IAppointmentService
{
    Task<Result<bool>> CreateAppointment(CreateAppointmentDto appointmentDto);
}