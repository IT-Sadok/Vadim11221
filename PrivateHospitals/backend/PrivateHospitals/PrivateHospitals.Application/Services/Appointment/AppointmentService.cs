using AutoMapper;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.Appointment;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Application.Services.Appointment;

public class AppointmentService(
    IAppointmentRepository _appointmentRepository,
    IDoctorRepository _doctorRepository,
    IMapper _mapper
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
        
        var doctor = _mapper.Map<Doctor>(doctorDto);

        var appointmnet = _mapper.Map<Core.Models.Appointment>(appointmentDto);
        appointmnet.AppointmentDate = appointmentDto.Date;
        appointmnet.DoctorId = doctor.Id;
        
        var result = await _appointmentRepository.CreateAppointmentAsync(appointmnet);

        if (result == false)
        {
            return Result<bool>.ErrorResponse(new List<string>() {"Something went wrong during creating the appointment"});
        }
        
        return Result<bool>.SuccessResponse(true);
    }
}