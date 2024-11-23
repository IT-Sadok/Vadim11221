using System.Net;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.Appointment;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using PrivateHospitals.Infrastructure.Interfaces.Patient;

namespace PrivateHospitals.Application.Services.Appointment;

public class AppointmentService(
    IAppointmentRepository _appointmentRepository,
    IDoctorRepository _doctorRepository,
    IMapper _mapper,
    IPatientRepository _patientRepository
): IAppointmentService
{
    
     public async Task<Result<bool>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, string patientId)
     { 
         
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient is null)
        {
            return Result<bool>.ErrorResponse(new List<string> { "Patient not found" });
        }
        
        var doctor = await _doctorRepository.GetDoctorByIdAsync(appointmentDto.DoctorId);
        if (doctor is null)
        {
            return Result<bool>.ErrorResponse(new List<string> { "Doctor not found" });
        }
        
        var isDoctorAvailable = await _doctorRepository.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date);

        if (!isDoctorAvailable)
        {
            return Result<bool>.ErrorResponse(new List<string> { "Doctor is not available" });
        }

        if (appointmentDto.Date < DateTime.Now)
        {
            return Result<bool>.ErrorResponse(new List<string> { "Appointment date has to be in the future" });
        }
        
        var appointment = _mapper.Map<Core.Models.Appointment>(appointmentDto);
        appointment.AppointmentDate = appointmentDto.Date;
        appointment.PatientId = patientId;
        
        await _appointmentRepository.CreateAppointmentAsync(appointment);
        
        return Result<bool>.SuccessResponse(true);
     }

     public async Task<Result<FilteredResult<AppointmentDto>>> GetAppointmentsAsync(AppointmentFilterDto appointmentFilterDto, string patientId)
     {
         if (appointmentFilterDto.FromDate.HasValue && appointmentFilterDto.ToDate.HasValue)
         {
             if (appointmentFilterDto.FromDate >= appointmentFilterDto.ToDate)
             {
                 return Result<FilteredResult<AppointmentDto>>.ErrorResponse(new List<string> { "From date cannot be greater than To date" });
             }
         }
         
         var patient = await _patientRepository.GetPatientByIdAsync(patientId);
         if (patient is null)
         {
             return Result<FilteredResult<AppointmentDto>>.ErrorResponse(new List<string> { "Patient not found" });
         }

         var appointmentFilter = new AppointmentFilter
         {
             Speciality = appointmentFilterDto.Speciality,
             ToDate = appointmentFilterDto.ToDate,
             FromDate = appointmentFilterDto.FromDate
         };

         var paging = new Paging
         {
            Page = appointmentFilterDto.Page,
            PageSize = appointmentFilterDto.PageSize
         };
         
         var appointments = await _appointmentRepository.GetAppointmentsByFilter(appointmentFilter, paging, patientId);
         
         var appointmentsDto = _mapper.Map<FilteredResult<AppointmentDto>>(appointments);

         return Result<FilteredResult<AppointmentDto>>.SuccessResponse(appointmentsDto);
     }

}