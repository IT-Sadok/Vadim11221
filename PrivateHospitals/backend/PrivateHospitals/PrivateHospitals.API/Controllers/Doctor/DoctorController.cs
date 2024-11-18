using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Interfaces.Doctor;
using PrivateHospitals.Application.Interfaces.WorkingHours;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.API.Controllers.Doctor;

[Route("api/doctors")]
[ApiController]
public class DoctorController(
    IDoctorService _doctorService
): ControllerBase
{
    [HttpPut("working-hours")]
    public async Task<IActionResult> UpdateWorkingHours([FromBody] DoctorWorkingHoursDto doctorWorkingHours)
    {
        var result = await _doctorService.UpdateWorkingHoursAsync(doctorWorkingHours.DoctorId, doctorWorkingHours.Schedule);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpGet("working-hours")]
    public async Task<IActionResult> GetWorkingHours([FromQuery] string doctorId)
    {
        var result = await _doctorService.GetWorkingHoursAsync(doctorId);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result);
    }
}