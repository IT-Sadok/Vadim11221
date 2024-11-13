using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Interfaces.Patient;

public interface IPatientRepository
{
    Task<AppUser> GetPatientByName(string firstName, string lastName);
}