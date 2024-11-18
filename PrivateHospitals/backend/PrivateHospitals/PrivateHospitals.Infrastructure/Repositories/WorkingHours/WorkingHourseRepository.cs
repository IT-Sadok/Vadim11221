using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.WorkingHours;

namespace PrivateHospitals.Infrastructure.Repositories.WorkingHours;

public class WorkingHourseRepository(
    HospitalDbContext _context
): IWorkingHourseRepository
{

}