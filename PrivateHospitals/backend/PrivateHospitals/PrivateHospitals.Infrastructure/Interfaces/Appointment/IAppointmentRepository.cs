using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Infrastructure.Interfaces.Appointment;

public interface IAppointmentRepository
{
    Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment);
    Task<List<Core.Models.Appointment>> GetAppointmentsBySpeciality(DoctorSpecialities speciality, string patientId);
    Task<List<Core.Models.Appointment>> GetAppointmentsByDate(DateOnly fromDate, DateOnly toDate, string patientId);
    Task<bool> IsDoctorHaveAppointment(DateOnly date, TimeSpan time, string doctorId);
}