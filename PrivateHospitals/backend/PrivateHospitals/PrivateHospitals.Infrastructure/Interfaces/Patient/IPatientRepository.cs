namespace PrivateHospitals.Infrastructure.Interfaces.Patient;

public interface IPatientRepository
{
    Task<bool> AddPatientAsync(Core.Models.Users.Patient patient);
}