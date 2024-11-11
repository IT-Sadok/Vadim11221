using Microsoft.AspNetCore.Identity;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Responses;

namespace PrivateHospitals.Application.Interfaces.User;

public interface IUserService
{
    Task<ServiceResponse<bool>> RegisterDoctorAsync(RegisterDoctorDto registerDoctorDto);
    Task<ServiceResponse<bool>> RegisterPatientAsync(RegisterPatientDto registerPatientDto);
    Task<ServiceResponse<UserLoggedDto>> LoginUserAsync(LoginDto loginDto);
}