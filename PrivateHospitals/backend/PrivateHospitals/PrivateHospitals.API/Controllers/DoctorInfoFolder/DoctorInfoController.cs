using Microsoft.AspNetCore.Mvc;
using PrivateHospitals.Application.Dtos.DoctorInfo;
using PrivateHospitals.Application.Interfaces.DoctorInfo;
using PrivateHospitals.Application.Services.DoctorInfo;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.API.Controllers.DoctorInfoFolder
{
    [ApiController]
    [Route("doctor-info")]
    public class DoctorInfoController: ControllerBase
    {
        private readonly IDoctorInfoService _doctorInfoService;

        public DoctorInfoController(IDoctorInfoService doctorInfoService)
        {
            _doctorInfoService = doctorInfoService;
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertDoctorInfo([FromBody] DoctorInfoDto doctorInfo)
        {
            if(doctorInfo == null)
            {
                return BadRequest("DoctorInfo is null");
            }

            var result = await _doctorInfoService.UpsertDoctorInfoAsync(doctorInfo);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest("Failed");

        }
    }
}
