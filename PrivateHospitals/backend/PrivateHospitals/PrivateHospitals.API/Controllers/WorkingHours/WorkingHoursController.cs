using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Interfaces.WorkingHours;

namespace PrivateHospitals.API.Controllers.WorkingHours;


[Route("api/working-hours")]
[ApiController]
public class WorkingHoursController(
    IWorkingHourseService _workingHourseService
): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddWorkingHourse([FromBody] AddWorkingHourseDto workingHourseDto)
    {
        var result = await _workingHourseService.AddWorkingHours(workingHourseDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}