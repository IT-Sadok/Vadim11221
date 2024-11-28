using Microsoft.EntityFrameworkCore;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;

namespace PrivateHospital.Migration.Dto.Repositories;

public class DoctorRepository(HospitalDbContext _context): IRepository<Doctor>
{
    public async Task<Doctor> GetByExternalId(string externalId)
    {
        return await _context.Users
            .OfType<Doctor>()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId);
    }

    public async Task AddAsync(Doctor entity)
    {   
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
    }
}