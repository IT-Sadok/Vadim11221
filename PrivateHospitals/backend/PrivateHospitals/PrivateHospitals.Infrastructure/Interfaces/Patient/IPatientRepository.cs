using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Interfaces.Patient;

public interface IPatientRepository
{
    Task<Core.Models.Users.Patient> GetPatientByIdAsync(string patientId);
}