using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Interfaces.Appointment;

public interface IAppointmentService
{
    Task<Result<bool>> CreateAppointment(CreateAppointmentDto appointmentDto);
    Task<Result<List<AppointmentDto>>> GetAppointmentsBySpecialityId(DoctorSpecialities speciality, string patientId);
}