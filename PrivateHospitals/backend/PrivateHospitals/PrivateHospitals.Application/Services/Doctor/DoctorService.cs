using System.Text.Json;
using AutoMapper;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Interfaces.Doctor;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Application.Services.Doctor;

public class DoctorService(
    IDoctorRepository _doctorRepository,
    IMapper _mapper
): IDoctorService
{
     public async Task<Result<bool>> UpdateWorkingHoursAsync(DoctorUpdateHours doctorDto)
     {
         if (doctorDto == null)
         {
             return Result<bool>.ErrorResponse(new List<string> { "Error, write doctor id and working hours" });
         }
         
         var doctor = await  _doctorRepository.GetDoctorByIdAsync(doctorDto.DoctorId);
         if (doctor == null)
         {
             return Result<bool>.ErrorResponse(new List<string> { "Doctor is not found" });
         }

         if (doctorDto.WorkingHours.Count == 0)
         {
             return Result<bool>.ErrorResponse(new List<string> { "Working hours cannot be empty" });
         }

         foreach (var item in doctorDto.WorkingHours)
         {
             if (item.StartTime > item.EndTime)
             {
                 return Result<bool>.ErrorResponse(new List<string> { "Start working hours cannot be greater than end time" });
             }
         }
         
         await _doctorRepository.UpdateWorkingHoursAsync(doctorDto.DoctorId, doctorDto.WorkingHours);
         
         return Result<bool>.SuccessResponse(true);
     }

     public async Task<Result<List<GetDoctorHoursDto>>> GetWorkingHoursAsync(string doctorId)
     {
         var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
         if (doctor == null)
         {
             return Result<List<GetDoctorHoursDto>>.ErrorResponse(new List<string>() {"Doctor is not found"});
         }
    
         if (doctor.WorkingHours.Count == 0)
         {
             return Result<List<GetDoctorHoursDto>>.ErrorResponse(new List<string>(){"Working hours is not found"});
         }
         
         var doctorWorkingHours = await _doctorRepository.GetWorkingHoursAsync(doctorId);

         var doctorWorkingHoursDto = _mapper.Map<List<GetDoctorHoursDto>>(doctorWorkingHours);
         
         return Result<List<GetDoctorHoursDto>>.SuccessResponse(doctorWorkingHoursDto);
     }
}