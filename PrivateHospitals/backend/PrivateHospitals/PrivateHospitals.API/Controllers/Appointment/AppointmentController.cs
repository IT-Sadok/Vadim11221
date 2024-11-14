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
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
    {
        var result = await _appointmentService.CreateAppointment(appointmentDto);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointmentsBySpeciality([FromQuery] DoctorSpecialities speciality, [FromQuery] string patientId)
    {
        var result = await _appointmentService.GetAppointmentsBySpecialityId(speciality, patientId);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
    
        return Ok(result);
    }

    [HttpGet("date")]
    public async Task<IActionResult> GetAppointmentByDate([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo, [FromQuery] string patientId)
    {
        var result = await _appointmentService.GetAppointmentByDate(dateFrom, dateTo, patientId);
        
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }
}