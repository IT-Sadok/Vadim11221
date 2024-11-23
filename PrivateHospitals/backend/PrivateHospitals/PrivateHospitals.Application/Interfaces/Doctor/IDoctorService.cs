using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Interfaces.Doctor;

public interface IDoctorService
{
    Task<Result<bool>> UpdateWorkingHoursAsync(DoctorUpdateHours doctorDto);
    Task<Result<List<GetDoctorHoursDto>>> GetWorkingHoursAsync(string doctorId);
}