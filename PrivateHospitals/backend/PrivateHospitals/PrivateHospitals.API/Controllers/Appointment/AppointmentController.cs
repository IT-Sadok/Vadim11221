using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.API.Controllers.Appointment;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(
    IAppointmentService _appointmentService
): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        var result = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAppointments(
        [FromQuery] DoctorSpecialities? speciality = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null
    )
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value 
                     ?? HttpContext.User.FindFirst("sub")?.Value; 

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }
        
        if (speciality.HasValue)
        {
            var result = await _appointmentService.GetAppointmentsBySpecialityAsync(userId, speciality.Value);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
        
        if (fromDate.HasValue && toDate.HasValue)
        {
            if (fromDate > toDate)
            {
                return BadRequest("The 'fromDate' cannot be greater than 'toDate'.");
            }

            var result = await _appointmentService.GetAppointmentsByDateAsync(userId, fromDate.Value, toDate.Value);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
    
        return BadRequest("At least one filter (speciality or date range) must be provided.");
    }

    
}