using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Infrastructure.Interfaces.Appointment;

public interface IAppointmentRepository
{
    Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment);
    Task<List<Core.Models.Appointment>> GetAppointmentsBySpeciality(DoctorSpecialities speciality, string patientId);
}