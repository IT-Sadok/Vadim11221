using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.WorkingHours;

namespace PrivateHospitals.Infrastructure.Repositories.WorkingHours;

public class WorkingHourseRepository(
    HospitalDbContext _context
): IWorkingHourseRepository
{
    public async Task<bool> AddWorkingHoursAsync(Core.Models.WorkingHours workingHours)
    {
        await _context.WorkingHours.AddAsync(workingHours);
        await _context.SaveChangesAsync();

        return true;
    }
}