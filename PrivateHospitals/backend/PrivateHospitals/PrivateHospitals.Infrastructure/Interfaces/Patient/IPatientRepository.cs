namespace PrivateHospitals.Data.Interfaces.Patient;
using PrivateHospitals.Core.Models;

public interface IPatientRepository
{
    Task<bool> AddPatientAsync(Patient patient);
}