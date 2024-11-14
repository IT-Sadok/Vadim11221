using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Patient;

namespace PrivateHospitals.Infrastructure.Repositories.Patient;

public class PatientRepository(
    HospitalDbContext _context
): IPatientRepository
{
    public async Task<Core.Models.Users.Patient> GetPatientByIdAsync(string patientId)
    {
        var patient = await _context.Users
            .OfType<Core.Models.Users.Patient>()
            .FirstOrDefaultAsync(x => x.Id == patientId);
        
        return patient;
    }
}