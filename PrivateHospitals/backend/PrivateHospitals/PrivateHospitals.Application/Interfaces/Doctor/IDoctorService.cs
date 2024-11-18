using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Interfaces.Doctor;

public interface IDoctorService
{
    Task<Result<bool>> UpdateWorkingHoursAsync(string doctorId,  Schedule schedule);
    Task<Result<Schedule>> GetWorkingHoursAsync(string doctorId);
}