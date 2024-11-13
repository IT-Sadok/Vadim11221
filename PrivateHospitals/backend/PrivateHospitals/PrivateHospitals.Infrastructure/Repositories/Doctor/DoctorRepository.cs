using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Infrastructure.Repositories.Doctor;

public class DoctorRepository(
    HospitalDbContext _context
): IDoctorRepository
{
    public async Task<AppUser> GetDoctorByFullName(string firstName, string lastName)
    {
        var doctor = await _context.Users
            .OfType<Core.Models.Users.Doctor>()
            .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
        
        return doctor;
    }
}