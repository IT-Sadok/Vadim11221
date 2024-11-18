using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Infrastructure.Interfaces.Appointment;

public interface IAppointmentRepository
{
    Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment);

    Task<List<Core.Models.Appointment>> GetAppointmentBySpecialityAsync(string patientId, DoctorSpecialities? speciality);
    Task<List<Core.Models.Appointment>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate);
}