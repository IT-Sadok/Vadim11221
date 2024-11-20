using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Interfaces.Doctor;


namespace PrivateHospitals.API.Controllers.Doctor;

[Route("api/doctors")]
[ApiController]
public class DoctorController(
    IDoctorService _doctorService
): ControllerBase
{
     [HttpPut("working-hours")]
     public async Task<IActionResult> UpdateWorkingHours([FromBody] DoctorUpdateHours doctorDto)
     {
         var result = await _doctorService.UpdateWorkingHoursAsync(doctorDto);
    
         if (!result.Success)
         {
             return BadRequest(result.Errors);
         }
    
         return Ok(result.Data);
     }

     [Authorize]
     [HttpGet("working-hours")]
     public async Task<IActionResult> GetWorkingHours()
     {
         var doctorId = HttpContext.User.FindFirst("userId")?.Value
                         ?? HttpContext.User.FindFirst("sub")?.Value;
         
         var result = await _doctorService.GetWorkingHoursAsync(doctorId);
         if (!result.Success)
         {
             return BadRequest(result.Errors);
         }
         
         return Ok(result);
     }
}