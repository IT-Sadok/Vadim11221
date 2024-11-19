using System.Text.Json;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Interfaces.Doctor;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Application.Services.Doctor;

public class DoctorService(
    IDoctorRepository _doctorRepository
): IDoctorService
{
    public async Task<Result<bool>> UpdateWorkingHoursAsync(string doctorId, Schedule schedule)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
        if (doctor == null)
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Doctor not found"});
        }
        
        if (schedule == null || !schedule.Days.Any())
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Problems with schedule"});
        }
        
        await _doctorRepository.UpdateWorkingHoursAsync(doctorId, schedule);
        
        return Result<bool>.SuccessResponse(true);
    }
    

    public async Task<Result<Schedule>> GetWorkingHoursAsync(string doctorId)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
        if (doctor == null)
        {
            return Result<Schedule>.ErrorResponse(new List<string>() {"Doctor not found"});
        }

        if (string.IsNullOrEmpty(doctor.WorkingHoursJson))
        {
            return Result<Schedule>.ErrorResponse(new List<string>(){"Working hours is not found"});
        }
        
        var schedule = JsonSerializer.Deserialize<Schedule>(doctor.WorkingHoursJson);
        return Result<Schedule>.SuccessResponse(schedule);
    }
}