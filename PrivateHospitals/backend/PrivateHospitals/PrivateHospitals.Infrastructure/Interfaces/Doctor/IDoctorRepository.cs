namespace PrivateHospitals.Infrastructure.Interfaces.Doctor;

public interface IDoctorRepository
{
    Task<bool> AddDoctorAsync(Core.Models.Users.Doctor doctor);
}