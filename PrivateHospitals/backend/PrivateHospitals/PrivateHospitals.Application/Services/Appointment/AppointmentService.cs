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

     public async Task<Result<PaginationResult<AppointmentDto>>> GetAppointmentsAsync(AppointmentFilter appointmentFilter, string patientId)
     {
         if (appointmentFilter.Speciality != null)
         {
             if (appointmentFilter.PageNumber == null || appointmentFilter.PageSize == 0)
             {
                 return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "Invalid page size or page number" });
             }

             var appointments = await _appointmentRepository.GetAppointmentBySpecialityAsync(appointmentFilter, patientId);
             if (appointments.Items.Count == 0)
             {
                 return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "No appointments found" });
             }
             
             var appointmentsDto = _mapper.Map<PaginationResult<AppointmentDto>>(appointments);
             
             return Result<PaginationResult<AppointmentDto>>.SuccessResponse(appointmentsDto);
         }
         else if (appointmentFilter.ToDate.HasValue && appointmentFilter.FromDate.HasValue)
         {
             if (appointmentFilter.PageNumber == null || appointmentFilter.PageSize == 0)
             {
                 return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "Invalid page size or page number" });
             }
             
             if (appointmentFilter.FromDate > appointmentFilter.ToDate)
             {
                 return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "From date cannot be greater than To date" });
             }
             
             var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(appointmentFilter, patientId);
             if (appointments.Items.Count == 0)
             {
                 return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "No appointments found" });
             }
             
             var appointmentsDto = _mapper.Map<PaginationResult<AppointmentDto>>(appointments);
             
             return Result<PaginationResult<AppointmentDto>>.SuccessResponse(appointmentsDto);
         }
         
         return Result<PaginationResult<AppointmentDto>>.ErrorResponse(new List<string> { "Something went wrong" });
     }

     // public async Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityAsync(string patientId, DoctorSpecialities? speciality)
    // {
    //     var patient = await _patientRepository.GetPatientByIdAsync(patientId);
    //     if (patient == null)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Patient not found"});
    //     }
    //
    //     if (speciality == null)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Doctor speciality can`t be null"});
    //     }
    //
    //     var appointments = await _appointmentRepository.GetAppointmentBySpecialityAsync(patientId, speciality);
    //     if (appointments.Count == 0)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> { "Appointment not found" });
    //     }
    //
    //     var appointmentDto = _mapper.Map<List<AppointmentDto>>(appointments);
    //     
    //     return Result<List<AppointmentDto>>.SuccessResponse(appointmentDto);
    // }
    //
    // public async Task<Result<List<AppointmentDto>>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate)
    // {
    //     var patient = await _patientRepository.GetPatientByIdAsync(patientId);
    //     if (patient == null)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Patient not found"});
    //     }
    //
    //     if (fromDate == null || toDate == null)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"FromDate and ToDate cannot be null"});
    //     }
    //
    //     var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(patientId, fromDate, toDate);
    //     if (appointments.Count == 0)
    //     {
    //         return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Appointment not found"});
    //     }
    //     
    //     var appointmentDto = _mapper.Map<List<AppointmentDto>>(appointments);
    //     
    //     return Result<List<AppointmentDto>>.SuccessResponse(appointmentDto);
    // }
}