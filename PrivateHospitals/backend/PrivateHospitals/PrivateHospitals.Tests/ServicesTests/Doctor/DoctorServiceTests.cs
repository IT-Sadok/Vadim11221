using System.Text.Json;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using Moq;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Interfaces.Doctor;
using PrivateHospitals.Application.Services.Doctor;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using Xunit;

namespace PrivateHospitals.Tests.ServicesTests.Doctor;

public class DoctorServiceTests
{
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DoctorService _doctorService;

        public DoctorServiceTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _mapperMock = new Mock<IMapper>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenDtoIsNull()
        {
            var result = await _doctorService.UpdateWorkingHoursAsync(null);

            result.Should().NotBeNull();
            result.Errors.Should().Contain("Error, write doctor id and working hours");
        }

        [Fact]
        public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenDoctorNotFound()
        {
            var doctorId = "doctorId";
            var doctorDto = new DoctorUpdateHours{ DoctorId = doctorId, WorkingHours = new List<WorkingHours>()};
            
            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync((Core.Models.Users.Doctor)null);
            
            var result = await _doctorService.UpdateWorkingHoursAsync(doctorDto);
            
            result.Should().NotBeNull();
            result.Errors.Should().Contain("Doctor is not found");
        }

        [Fact]
        public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenWorkingHoursAreEmpty()
        {
            var doctorId = "doctorId";
            var doctor = new Core.Models.Users.Doctor {Id = doctorId};
            var doctorDto = new DoctorUpdateHours{ DoctorId = doctorId, WorkingHours = new List<WorkingHours>()};

            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(doctor);
            
            var result = await _doctorService.UpdateWorkingHoursAsync(doctorDto);
            
            result.Should().NotBeNull();
            result.Errors.Should().Contain("Working hours cannot be empty");
        }

        [Fact]
        public async Task UpdateWorkingHoursAsync_ShouldReturnError_WhenStartTimeIsGreaterThanEndTime()
        {
            var doctorId = "doctorId";
            var doctor = new Core.Models.Users.Doctor {Id = doctorId};
            var doctorDto = new DoctorUpdateHours
            {
                DoctorId = doctorId, 
                WorkingHours = new List<WorkingHours>
                {
                    new WorkingHours()
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = new TimeSpan(18, 0, 0),
                        EndTime = new TimeSpan(9, 0, 0)
                    }
                }
            };

            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(doctor);
            
            var result = await _doctorService.UpdateWorkingHoursAsync(doctorDto);
            
            result.Should().NotBeNull();
            result.Errors.Should().Contain("Start working hours cannot be greater than end time");
        }

        [Fact]
        public async Task UpdateWorkingHoursAsync_ShouldReturnSuccess_WhenValidDataIsProvided()
        {
            var doctorId = "doctorId";
            var doctor = new Core.Models.Users.Doctor {Id = doctorId};
            var doctorDto = new DoctorUpdateHours
            {
                DoctorId = doctorId, 
                WorkingHours = new List<WorkingHours>
                {
                    new WorkingHours()
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = new TimeSpan(10, 0, 0),
                        EndTime = new TimeSpan(19, 0, 0)
                    }
                }
            };

            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(doctor);
            
            var result = await _doctorService.UpdateWorkingHoursAsync(doctorDto);
            
            result.Should().NotBeNull();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task GetWorkingHoursAsync_ShouldReturnError_WhenDoctorNotFound()
        {
            var doctorId = "doctorId";
            
            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync((Core.Models.Users.Doctor)null);
            
            var result = await _doctorService.GetWorkingHoursAsync(doctorId);
            
            result.Should().NotBeNull();
            result.Errors.Should().Contain("Doctor is not found");
        }

        [Fact]
        public async Task GetWorkingHoursAsync_ShouldReturnError_WhenWorkingHoursAreEmpty()
        {
            var doctorId = "doctorId";
            var doctor = new Core.Models.Users.Doctor { Id = doctorId };
            doctor.WorkingHours = new List<WorkingHours>();
            
            _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(doctor);
            
            var result = await _doctorService.GetWorkingHoursAsync(doctorId);
            
            result.Should().NotBeNull();
            result.Errors.Should().Contain("Working hours is not found");
        }
        
    [Fact]
    public async Task GetWorkingHoursAsync_ShouldReturnSuccess_WhenDoctorHasWorkingHours()
    {
        var doctorId = "doctorId";
        
        var workingHours = new List<Core.Models.WorkingHours>
        {
            new Core.Models.WorkingHours
            {
                Day = DayOfWeek.Monday,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(18, 0, 0)
            }
        };

        var doctor = new Core.Models.Users.Doctor
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            DoctorSpeciality = DoctorSpecialities.Doctor,
            WorkingHours = workingHours
        };
        
        var doctorsList = new List<Core.Models.Users.Doctor> { doctor };
        
        var expectedDto = new List<GetDoctorHoursDto>
        {
            new GetDoctorHoursDto
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Speciality = doctor.DoctorSpeciality,
                WorkingHours = doctor.WorkingHours
            }
        };
        
        _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(doctorId))
            .ReturnsAsync(doctor);

        _doctorRepositoryMock.Setup(x => x.GetWorkingHoursAsync(doctorId))
            .ReturnsAsync(doctorsList);

        _mapperMock.Setup(x => x.Map<List<GetDoctorHoursDto>>(doctorsList))
            .Returns(expectedDto);


        var result = await _doctorService.GetWorkingHoursAsync(doctorId);


        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
    }


}