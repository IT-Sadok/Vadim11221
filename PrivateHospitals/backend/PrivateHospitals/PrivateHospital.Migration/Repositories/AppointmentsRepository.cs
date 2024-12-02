using Microsoft.EntityFrameworkCore;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Interfaces;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Data;

namespace PrivateHospital.Migration.Dto.Repositories;

public class AppointmentsRepository(HospitalDbContext _context): IRepository<Appointment>, IIdRepository<Appointment>
{
    public async Task<Appointment> GetByExternalId(string externalId)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.ExternalId == externalId);
    }

    public async Task AddAsync(Appointment entity)
    {
        _context.Appointments.Add(entity);
        await _context.SaveChangesAsync();
    }

}