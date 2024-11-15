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

    public async Task<bool> IsDoctorOnWork(DateOnly date, TimeSpan time, string doctorId)
    {
        var dateWork = await _context.WorkingHours
            .FirstOrDefaultAsync(x => x.Date == date && x.StartTime <= time && x.EndTime >= time && x.DoctorId == doctorId);

        if (dateWork == null)
        {
            return false;
        }

        return true;
    }
    
}