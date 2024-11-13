using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Interfaces.Appointment;

namespace PrivateHospitals.API.Controllers.Appointment;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(
    IAppointmentService _appointmentService
): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
    {
        var result = await _appointmentService.CreateAppointment(appointmentDto);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result);
    }
}