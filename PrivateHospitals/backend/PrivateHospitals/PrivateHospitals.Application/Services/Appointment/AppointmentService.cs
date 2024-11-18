using System.Net;
using System.Text.Json;
using AutoMapper;
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
    public async Task<Result<bool>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(appointmentDto.DoctorId);
        if (doctor == null)
        {
            return Result<bool>.ErrorResponse(new List<string> {"Doctor not found"}); 
        }
        
        var patient = await _patientRepository.GetPatientByIdAsync(appointmentDto.PatientId);
        if (patient == null)
        {
            return Result<bool>.ErrorResponse(new List<string> {"Patient not found"});
        }

        var isDoctorAvailable = await _doctorRepository.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date);

        if (isDoctorAvailable == false)
        {
            return Result<bool>.ErrorResponse(new List<string> {"Doctor is not available"});
        }
        
        var appointment = _mapper.Map<Core.Models.Appointment>(appointmentDto);
        appointment.AppointmentDate = appointmentDto.Date.ToUniversalTime();

        _appointmentRepository.CreateAppointmentAsync(appointment);

        return Result<bool>.SuccessResponse(true);
    }

    public async Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityAsync(string patientId, DoctorSpecialities? speciality)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Patient not found"});
        }

        var appointments = await _appointmentRepository.GetAppointmentBySpecialityAsync(patientId, speciality);
        if (appointments.Count == 0)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string> { "Appointment not found" });
        }

        var appointmentDto = _mapper.Map<List<AppointmentDto>>(appointments);
        
        return Result<List<AppointmentDto>>.SuccessResponse(appointmentDto);
    }

    public async Task<Result<List<AppointmentDto>>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Patient not found"});
        }

        var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(patientId, fromDate, toDate);
        if (appointments.Count == 0)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string> {"Appointment not found"});
        }
        
        var appointmentDto = _mapper.Map<List<AppointmentDto>>(appointments);
        
        return Result<List<AppointmentDto>>.SuccessResponse(appointmentDto);
    }
}