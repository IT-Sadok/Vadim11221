using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Interfaces.Appointment;

public interface IAppointmentService
{
    Task<Result<bool>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, string patientId);
    // Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityAsync(string patientId, DoctorSpecialities? speciality);
    // Task<Result<List<AppointmentDto>>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate);
    Task<Result<PaginationResult<AppointmentDto>>> GetAppointmentsAsync(AppointmentFilter appointmentFilter, string patientId);
}