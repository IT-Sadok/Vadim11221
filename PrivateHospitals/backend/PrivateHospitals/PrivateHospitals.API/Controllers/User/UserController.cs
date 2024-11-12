using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Interfaces.User;
using LoginDto = PrivateHospitals.Application.Dtos.User.LoginDto;

namespace PrivateHospitals.API.Controllers.User;

[Route("api/users")]
[ApiController]
public class UserController(
    IUserService _userService
): ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
    {
        var result = await _userService.RegisterUser(registerDto);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Data);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
    {
       var result = await _userService.LoginUserAsync(loginDto);

       if (!result.Success)
       {
           return BadRequest(result.Errors);
       }
       
       return Ok(result);
    }
}