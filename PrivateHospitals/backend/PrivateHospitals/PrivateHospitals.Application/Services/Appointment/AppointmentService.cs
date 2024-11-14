using AutoMapper;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;
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
    public async Task<Result<bool>> CreateAppointment(CreateAppointmentDto appointmentDto)
    {
        var doctorDto = await _doctorRepository
            .GetDoctorByFullName(appointmentDto.DoctorFirstName, appointmentDto.DoctorLastName);
        

        if (doctorDto == null)
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Doctor not found"});
        }
        

        var appointmnet = _mapper.Map<Core.Models.Appointment>(appointmentDto);
        
        appointmnet.AppointmentDate = appointmentDto.Date;
        appointmnet.DoctorId = doctorDto.Id;
        
        var result = await _appointmentRepository.CreateAppointmentAsync(appointmnet);

        if (result == false)
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Something went wrong during creating the appointment"});
        }
        
        return Result<bool>.SuccessResponse(true);
    }

    public async Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityId(DoctorSpecialities speciality, string patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        

        if (patient == null)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string>() {"Patient not found"});
        }
        
        var appointments = await _appointmentRepository.GetAppointmentsBySpeciality(speciality, patientId);
        

        if (appointments.Count == 0)
        {
            return Result<List<AppointmentDto>>.ErrorResponse(new List<string>() {"Appointments not found"});
        }
        
        var appointmentsDto = _mapper.Map<List<AppointmentDto>>(appointments);
        
        return Result<List<AppointmentDto>>.SuccessResponse(appointmentsDto);
    }
}