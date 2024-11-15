using AutoMapper;
using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Interfaces.WorkingHours;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Infrastructure.Interfaces.WorkingHours;

namespace PrivateHospitals.Application.Services.WorkingHours;

public class WorkingHoursService(
    IWorkingHourseRepository _workingHourseRepository, 
    IMapper _mapper
): IWorkingHourseService
{
    public async Task<Result<bool>> AddWorkingHours(AddWorkingHourseDto workingHoursDto)
    {
        var hours = _mapper.Map<Core.Models.WorkingHours>(workingHoursDto);
        await _workingHourseRepository.AddWorkingHoursAsync(hours);
        
        return Result<bool>.SuccessResponse(true);
    }
}