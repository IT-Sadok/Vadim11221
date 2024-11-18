using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Interfaces.Appointment;

public interface IAppointmentService
{
    Task<Result<bool>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
    Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityAsync(string patientId, DoctorSpecialities? speciality);
    Task<Result<List<AppointmentDto>>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate);
}