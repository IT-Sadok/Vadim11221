using Microsoft.AspNetCore.Identity;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Responses;

namespace PrivateHospitals.Application.Interfaces.User;

public interface IUserService
{
    Task<Result<bool>> RegisterUser(RegisterDto regiisterDto);
    Task<Result<UserLoggedDto>> LoginUserAsync(LoginDto loginDto);
}
