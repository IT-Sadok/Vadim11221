using System.Text.Json;
using AutoMapper;
using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Interfaces.WorkingHours;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using PrivateHospitals.Infrastructure.Interfaces.WorkingHours;

namespace PrivateHospitals.Application.Services.WorkingHours;

public class WorkingHoursService(
    IDoctorRepository _doctorRepository, 
    IMapper _mapper
): IWorkingHourseService
{
    public async Task<Result<bool>> UpdateWorkingHoursAsync(string doctorId, Schedule schedule)
    {
        var doctor = await _doctorRepository.UpdateWorkingHoursAsync(doctorId, schedule);
        if (doctor == null)
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Doctor not found"});
        }
        
        if (schedule == null || !schedule.Days.Any())
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Schedule cannot be empty"});
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
        
        return JsonSerializer.Deserialize<Result<Schedule>>(doctor.WorkingHoursJson);
    }
}