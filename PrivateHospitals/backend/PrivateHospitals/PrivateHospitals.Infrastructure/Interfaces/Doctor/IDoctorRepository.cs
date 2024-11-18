using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Interfaces.Doctor;

public interface IDoctorRepository
{
    Task<Core.Models.Users.Doctor> GetDoctorByIdAsync(string doctorId );
    Task<bool> UpdateWorkingHoursAsync(string doctorId,  Schedule schedule);
    Task<bool> IsDoctorAvailableAsync(string doctorId, DateTime appointmentDate);

}