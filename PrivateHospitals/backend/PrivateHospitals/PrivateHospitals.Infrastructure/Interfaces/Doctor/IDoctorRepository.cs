using PrivateHospitals.Core.Models;


namespace PrivateHospitals.Infrastructure.Interfaces.Doctor;

public interface IDoctorRepository
{
    Task<Core.Models.Users.Doctor> GetDoctorByIdAsync(string doctorId );
    Task<bool> UpdateWorkingHoursAsync(string doctorId,  List<WorkingHours> workingHours);
    Task<bool> IsDoctorAvailableAsync(string doctorId, DateTime appointmentDate);
    Task<List<Core.Models.Users.Doctor>> GetWorkingHoursAsync(string doctorId);

}   