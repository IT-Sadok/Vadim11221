using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Interfaces.Appointment;

public interface IAppointmentService
{
    Task<Result<bool>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, string patientId);
    Task<Result<FilteredResult<AppointmentDto>>> GetAppointmentsAsync(AppointmentFilterDto appointmentFilterDto, string patientId);
}