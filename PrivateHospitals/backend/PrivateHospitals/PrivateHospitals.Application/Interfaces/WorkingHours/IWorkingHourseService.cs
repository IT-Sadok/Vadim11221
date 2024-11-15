using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Responses;

namespace PrivateHospitals.Application.Interfaces.WorkingHours;

public interface IWorkingHourseService
{
    Task<Result<bool>> AddWorkingHours(AddWorkingHourseDto workingHoursDto);
}