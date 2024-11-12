using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Interfaces;
using PrivateHospitals.Application.Interfaces.Token;
using PrivateHospitals.Application.Interfaces.User;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.User;

namespace PrivateHospitals.Application.Services.User;

public class UserService(
    IUserRepository _userRepository,
    SignInManager<AppUser> _signInManager,
    ITokenService _tokenService,
    IMapper _mapper
): IUserService
{
    
    public async Task<Result<bool>> RegisterUser(RegisterDto registerDto)
    {
        if (registerDto.Role == Roles.Doctor)
        {
            var doctor = _mapper.Map<Doctor>(registerDto);
            
            if (doctor is null)
            {
                return Result<bool>.ErrorResponse(new List<string> { "Doctor is null" });
            }
            
            var result = await _userRepository.CreateUserAsync(doctor, registerDto.Password);
            if (!result.Succeeded)
            {
                return Result<bool>.ErrorResponse(new List<string> { "Something went wrong during creating the doctor." });
            }
            
            await _userRepository.AddUserToRoleAsync(doctor, Roles.Doctor);
        }
        else if (registerDto.Role == Roles.Patient)
        {
            var patient = _mapper.Map<Patient>(registerDto);

            if (patient is null)
            {
                return Result<bool>.ErrorResponse(new List<string> { "Patient is null" });
            }
            
            var result = await _userRepository.CreateUserAsync(patient, registerDto.Password);
            if (!result.Succeeded)
            {
                return Result<bool>.ErrorResponse(new List<string> { "Something went wrong during creating the patient." });
            }
            await _userRepository.AddUserToRoleAsync(patient, Roles.Patient);
        }
        
        return Result<bool>.SuccessResponse(true);
    }

    public async Task<Result<UserLoggedDto>> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return Result<UserLoggedDto>.ErrorResponse(new List<string> { "User not found" });
        }
        
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if (!signInResult.Succeeded)
        {
            return Result<UserLoggedDto>.ErrorResponse(new List<string> { "Invalid login or password" });
        }
        
        return Result<UserLoggedDto>.SuccessResponse(new UserLoggedDto
        {
            Email = loginDto.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}