using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Interfaces.Statistics;

namespace PrivateHospitals.API.Controllers.Statistics
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController: ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("doctors/speciality")]
        public async Task<IActionResult> GetDoctorCountBySpeality()
        { 
            var result = await _statisticsService.GetDoctorCountBySpecialityAsync();

            return Ok(result);
        }

        [HttpGet("doctors/years-of-experience")]
        public async Task<IActionResult> GetDoctorYearsOfExperience()
        {
            var result = await _statisticsService.GetDoctorYearsOfExperiencesAsync();

            return Ok(result);
        }

        [HttpGet("doctors/quantiles/years")]
        public async Task<IActionResult> GetDoctorYearsQuantiles()
        {
            var result = await _statisticsService.GetDoctorYearsQuantilesAsync();

            return Ok(result);
        }

        [HttpGet("doctors/avg/appointments")]
        public async Task<IActionResult> GetDoctorAVgAppointments()
        {
            var result = await _statisticsService.GetDoctorAvgAppointmentsAsync();

            return Ok(result);
        }

        [HttpGet("doctors/appointments")]
        public async Task<IActionResult> GetDoctorAppointmentsMoreOne()
        {
            var result = await _statisticsService.GetDoctorAppointmentsMoreOneAsync();

            return Ok(result);
        }
    }
}
