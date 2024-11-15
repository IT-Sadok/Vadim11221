using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PrivateHospitals.Application.Dtos.WorkingHours;
using PrivateHospitals.Application.Responses;
using PrivateHospitals.Application.Services.WorkingHours;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.WorkingHours;
using Xunit;
using Assert = Xunit.Assert;

public class WorkingHoursServiceTests
{
    private readonly Mock<IWorkingHourseRepository> _workingHoursRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly WorkingHoursService _workingHoursService;

    public WorkingHoursServiceTests()
    {
        _workingHoursRepositoryMock = new Mock<IWorkingHourseRepository>();
        _mapperMock = new Mock<IMapper>();

        _workingHoursService = new WorkingHoursService(
            _workingHoursRepositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task AddWorkingHours_ShouldReturnSuccess_WhenValidDataProvided()
    {
        var workingHoursDto = new AddWorkingHourseDto
        {
            DoctorId = "doctor123",
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(17)
        };

        var workingHours = new WorkingHours
        {
            DoctorId = "doctor123",
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(17)
        };

        _mapperMock
            .Setup(mapper => mapper.Map<WorkingHours>(workingHoursDto))
            .Returns(workingHours);

        _workingHoursRepositoryMock
            .Setup(repo => repo.AddWorkingHoursAsync(workingHours))
            .ReturnsAsync(true); // Повертає Task<bool>
        
        var result = await _workingHoursService.AddWorkingHours(workingHoursDto);
        
        Assert.True(result.Success);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task AddWorkingHours_ShouldThrowException_WhenRepositoryFails()
    {
        var workingHoursDto = new AddWorkingHourseDto
        {
            DoctorId = "doctor123",
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(17)
        };

        var workingHours = new WorkingHours
        {
            DoctorId = "doctor123",
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(17)
        };

        _mapperMock
            .Setup(mapper => mapper.Map<WorkingHours>(workingHoursDto))
            .Returns(workingHours);

        _workingHoursRepositoryMock
            .Setup(repo => repo.AddWorkingHoursAsync(workingHours))
            .ThrowsAsync(new Exception("Database error"));
        
        var exception = await Assert.ThrowsAsync<Exception>(() => _workingHoursService.AddWorkingHours(workingHoursDto));
        Assert.Equal("Database error", exception.Message);
    }
}
