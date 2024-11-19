using System.Text.Json;
using FluentAssertions;
using Moq;
using PrivateHospitals.Application.Services.Doctor;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using Xunit;

namespace PrivateHospitals.Tests.ServicesTests.Doctor;

public class DoctorServiceTests
{
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly DoctorService _doctorService;

    public DoctorServiceTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateWorkingHoursAsync_ShouldReturnTrue()
    {
        var doctorId = "1";
        Schedule schedule = new Schedule
        {
            Days = new Dictionary<string, WorkingHours>
            {
                { "Monday", new WorkingHours { StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(9) } },
                { "Tuesday", new WorkingHours { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(20) } }
            }
        };

        var doctor = new Core.Models.Users.Doctor{ Id = doctorId };

        _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync(doctor);
        
        var result = await _doctorService.UpdateWorkingHoursAsync(doctorId, schedule);

        result.Should().NotBeNull();
        result.Data.Should().BeTrue();
        result.Errors.Should().BeEmpty();   
    }
    
    [Fact]
    public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenDoctorNotFound()
    {
        var doctorId = "1";
        Schedule schedule = new Schedule
        {
            Days = new Dictionary<string, WorkingHours>
            {
                { "Monday", new WorkingHours { StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(9) } },
                { "Tuesday", new WorkingHours { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(20) } }
            }
        };

        _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync((Core.Models.Users.Doctor)null);
        
        var result = await _doctorService.UpdateWorkingHoursAsync(doctorId, schedule);

        result.Should().NotBeNull();
        result.Errors.Should().Contain("Doctor not found");   
    }
    
    [Fact]
    public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenScheduleNotFound()
    {
        var doctorId = "1";

        var doctor = new Core.Models.Users.Doctor{ Id = doctorId };

        _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync(doctor);
        
        var result = await _doctorService.UpdateWorkingHoursAsync(doctorId, null);

        result.Should().NotBeNull();
        result.Errors.Should().Contain("Problems with schedule");   
    }

    [Fact]
    public async Task GetWorkingHoursAsync_ShouldReturnError_WhenDoctorNotFound()
    {
        var doctorId = "1";

        _doctorRepositoryMock
            .Setup(repo => repo.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync((Core.Models.Users.Doctor)null);
        
        var result = await _doctorService.GetWorkingHoursAsync(doctorId);
        
        result.Should().NotBeNull();
        result.Errors.Should().Contain("Doctor not found");
        _doctorRepositoryMock.Verify(repo => repo.GetDoctorByIdAsync(doctorId), Times.Once);
    }

    [Fact]
    public async Task GetWorkingHoursAsync_ShouldReturnError_WhenWorkingHoursIsEmpty()
    {
        var doctorId = "1";
        var doctor = new Core.Models.Users.Doctor { Id = doctorId, WorkingHoursJson = null };

        _doctorRepositoryMock
            .Setup(repo => repo.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync(doctor);
        
        var result = await _doctorService.GetWorkingHoursAsync(doctorId);
        
        result.Should().NotBeNull();
        result.Errors.Should().Contain("Working hours is not found");
        _doctorRepositoryMock.Verify(repo => repo.GetDoctorByIdAsync(doctorId), Times.Once);
    }

    [Fact]
    public async Task GetWorkingHoursAsync_ShouldReturnSchedule_WhenDataIsValid()
    {
        var doctorId = "1";
        var schedule = new Schedule
        {
            Days = new Dictionary<string, WorkingHours>
            {
                { "Monday", new WorkingHours { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(17) } },
                { "Tuesday", new WorkingHours { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(18) } }
            }
        };

        var doctor = new Core.Models.Users.Doctor
        {
            Id = doctorId,
            WorkingHoursJson = JsonSerializer.Serialize(schedule)
        };

        _doctorRepositoryMock
            .Setup(repo => repo.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync(doctor);
        
        var result = await _doctorService.GetWorkingHoursAsync(doctorId);
        
        result.Should().NotBeNull();
        _doctorRepositoryMock.Verify(repo => repo.GetDoctorByIdAsync(doctorId), Times.Once);
    }
}