namespace PrivateHospitals.Data.Interfaces.Doctor;

public interface IDoctorRepository
{
    Task<bool> AddDoctorAsync(Core.Models.Doctor doctor);
}