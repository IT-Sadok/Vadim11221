using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Interfaces.User;
using LoginDto = PrivateHospitals.Application.Dtos.User.LoginDto;

namespace PrivateHospitals.API.Controllers.User;

[Route("api/user")]
[ApiController]
public class UserController(
    IUserService userService
): ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("RegisterDoctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto registerDoctorDto)
    {
        var result = await _userService.RegisterDoctorAsync(registerDoctorDto);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Data);
    }

    [HttpPost("RegisterPatient")]
    public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto registerPatientDto)
    {
        var result = await _userService.RegisterPatientAsync(registerPatientDto);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpPost("LoginUser")]
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