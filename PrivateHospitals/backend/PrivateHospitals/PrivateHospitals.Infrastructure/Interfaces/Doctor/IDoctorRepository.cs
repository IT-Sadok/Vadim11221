using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Interfaces.Doctor;

public interface IDoctorRepository
{
    Task<AppUser> GetDoctorByFullName(string firstName, string lastName );
    Task<bool> IsDoctorOnWork(DateOnly date, TimeSpan time, string doctorId);
}