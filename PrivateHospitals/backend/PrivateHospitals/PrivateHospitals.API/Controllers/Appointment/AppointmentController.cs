using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.API.Controllers.Appointment;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(
    IAppointmentService _appointmentService
): ControllerBase
{
    [Authorize]
    [HttpPost]
     public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
     {
         var patientId = HttpContext.User.FindFirst("userId")?.Value
             ?? HttpContext.User.FindFirst("sub")?.Value;
         
         var result = await _appointmentService.CreateAppointmentAsync(createAppointmentDto, patientId);
    
         if (!result.Success)
         {
             return BadRequest(result.Errors);
         }
    
         return Ok(result.Data);
     }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilterDto appointmentFilter)
    {
        var patientId = HttpContext.User.FindFirst("userId")?.Value
                        ?? HttpContext.User.FindFirst("sub")?.Value;
        
        var result = await _appointmentService.GetAppointmentsAsync(appointmentFilter, patientId);
        
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
    
        return Ok(result.Data);
    }
    
}