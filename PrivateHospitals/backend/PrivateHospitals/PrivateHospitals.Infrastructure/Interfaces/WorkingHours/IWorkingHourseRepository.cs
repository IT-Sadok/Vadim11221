namespace PrivateHospitals.Infrastructure.Interfaces.WorkingHours;

public interface IWorkingHourseRepository
{
    Task<bool> AddWorkingHoursAsync(Core.Models.WorkingHours workingHours);
}