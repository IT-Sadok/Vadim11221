using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Infrastructure.Interfaces.Appointment;

public interface IAppointmentRepository
{
    Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment);
    Task<PaginationResult<Core.Models.Appointment>> GetAppointmentBySpecialityAsync(AppointmentFilter appointmentFilter, string patientId);
    Task<PaginationResult<Core.Models.Appointment>> GetAppointmentsByDateAsync(AppointmentFilter appointmentFilter, string patientId);
}