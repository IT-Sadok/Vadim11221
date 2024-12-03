using Microsoft.EntityFrameworkCore;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Interfaces;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;

namespace PrivateHospital.Migration.Dto.Repositories;

public class PatientRepository(HospitalDbContext _context): IRepository<Patient>, IExternalIdRepository<Patient>
{
    public async Task<Patient> GetByExternalId(string externalId)
    {
        return await _context.Users
            .OfType<Patient>()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId);
    }

    public async Task AddAsync(Patient entity)
    {
        _context.Users.Add(entity);
    }
}