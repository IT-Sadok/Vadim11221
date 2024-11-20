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
    public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilter appointmentFilter)
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
     
    // [Authorize] 
    // [HttpGet]
    // public async Task<IActionResult> GetAppointments(
    //     [FromQuery] DoctorSpecialities? speciality = null,
    //     [FromQuery] DateTime? fromDate = null,
    //     [FromQuery] DateTime? toDate = null
    // )
    // {
    //     var userId = HttpContext.User.FindFirst("userId")?.Value 
    //                  ?? HttpContext.User.FindFirst("sub")?.Value; 
    //     
    //     if (speciality.HasValue)
    //     {
    //         var result = await _appointmentService.GetAppointmentsBySpecialityAsync(userId, speciality.Value);
    //
    //         if (!result.Success)
    //         {
    //             return BadRequest(result.Errors);
    //         }
    //
    //         return Ok(result.Data);
    //     }
    //     
    //     if (fromDate.HasValue && toDate.HasValue)
    //     {
    //         if (fromDate > toDate)
    //         {
    //             return BadRequest("The 'fromDate' cannot be greater than 'toDate'.");
    //         }
    //
    //         var result = await _appointmentService.GetAppointmentsByDateAsync(userId, fromDate.Value, toDate.Value);
    //
    //         if (!result.Success)
    //         {
    //             return BadRequest(result.Errors);
    //         }
    //
    //         return Ok(result.Data);
    //     }
    //
    //     return BadRequest("At least one filter (speciality or date range) must be provided.");
    // }

    
}