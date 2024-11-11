using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Interfaces;
using PrivateHospitals.Application.Interfaces.User;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Data.Interfaces.User;

namespace PrivateHospitals.Application.Services.User;

public class UserService(
    IUserRepository userRepository,
    SignInManager<AppUser> signInManager,
    ITokenService tokenService,
    IMapper mapper
): IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService; 
    private readonly IMapper _mapper = mapper;
    
    public async Task<ServiceResponse<bool>> RegisterDoctorAsync(RegisterDoctorDto registerDoctorDto)
    {
        var doctor = _mapper.Map<Doctor>(registerDoctorDto);

        if (doctor is null)
        {
            return ServiceResponse<bool>.ErrorResponse(new List<string> { "Doctor is null" });
        }
        
        await _userRepository.CreateUserAsync(doctor, registerDoctorDto.Password);
        await _userRepository.AddUserToRoleAsync(doctor, Roles.Doctor);

        return ServiceResponse<bool>.SuccessResponse(true);
    }

    public async Task<ServiceResponse<bool>> RegisterPatientAsync(RegisterPatientDto registerPatientDto)
    {
        var patient = _mapper.Map<Patient>(registerPatientDto);

        if (patient is null)
        {
            return ServiceResponse<bool>.ErrorResponse(new List<string> { "Patient is null" });
        }
        
        await _userRepository.CreateUserAsync(patient, registerPatientDto.Password);
        await _userRepository.AddUserToRoleAsync(patient, Roles.Patient);
        
        return ServiceResponse<bool>.SuccessResponse(true);
    }

    public async Task<ServiceResponse<UserLoggedDto>> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return ServiceResponse<UserLoggedDto>.ErrorResponse(new List<string> { "User not found" });
        }
        
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if (!signInResult.Succeeded)
        {
            return ServiceResponse<UserLoggedDto>.ErrorResponse(new List<string> { "Invalid login or password" });
        }
        
        return ServiceResponse<UserLoggedDto>.SuccessResponse(new UserLoggedDto
        {
            Email = loginDto.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}