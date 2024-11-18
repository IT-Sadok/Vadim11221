using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Interfaces.WorkingHours;

public interface IWorkingHourseService
{
    Task<Result<bool>> UpdateWorkingHoursAsync(string doctorId, Schedule schedule);
    Task<Result<Schedule>> GetWorkingHoursAsync(string doctorId);
}