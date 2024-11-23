 using AutoMapper;
 using FluentAssertions;
 using Moq;
 using PrivateHospitals.Application.Dtos.Appointment;
 using PrivateHospitals.Application.Interfaces.Appointment;
 using PrivateHospitals.Application.Services.Appointment;
 using PrivateHospitals.Core.Enum;
 using PrivateHospitals.Core.Models;
 using PrivateHospitals.Core.Models.Users;
 using PrivateHospitals.Infrastructure.Interfaces.Appointment;
 using PrivateHospitals.Infrastructure.Interfaces.Doctor;
 using PrivateHospitals.Infrastructure.Interfaces.Patient;
 using Xunit;
 using Assert = Xunit.Assert;

 public class AppointmentServiceTests
 {
      private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
      private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
      private readonly Mock<IMapper> _mapperMock;
      private readonly Mock<IPatientRepository> _patientRepositoryMock;
      private readonly IAppointmentService _appointmentService;
     
      public AppointmentServiceTests()
      {
          _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
          _doctorRepositoryMock = new Mock<IDoctorRepository>();
          _patientRepositoryMock = new Mock<IPatientRepository>();
          _mapperMock = new Mock<IMapper>();
          _appointmentService = new AppointmentService(
              _appointmentRepositoryMock.Object,
              _doctorRepositoryMock.Object,
              _mapperMock.Object,
              _patientRepositoryMock.Object
          );
      }
      
      [Fact]
      public async Task CreateAppointmentAsync_ShouldReturnSuccess_WhenAppointmentIsValid()
      {
          var appointmentDto = new CreateAppointmentDto
          {
              Date = DateTime.Now.AddDays(1),
              DoctorId = "doctorId"
          };
          var patientId = "patientId";

          var patient = new Patient { Id = patientId };
          var doctor = new Doctor { Id = appointmentDto.DoctorId };

          _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
              .ReturnsAsync(patient);

          _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
              .ReturnsAsync(doctor);

          _doctorRepositoryMock.Setup(x => x.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date))
              .ReturnsAsync(true);

          var appointment = new Appointment
          {
              AppointmentDate = appointmentDto.Date,
              DoctorId = appointmentDto.DoctorId,
              PatientId = patientId
          };

          _mapperMock.Setup(x => x.Map<Appointment>(appointmentDto))
              .Returns(appointment);

          _appointmentRepositoryMock.Setup(x => x.CreateAppointmentAsync(appointment))
              .ReturnsAsync(true);
          
          var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
            
          result.Should().NotBeNull();
          result.Success.Should().BeTrue();
          result.Data.Should().BeTrue();
      }
      
      [Fact]
      public async Task CreateAppointmentAsync_ShouldReturnError_WhenPatientNotFound()
      {
          var appointmentDto = new CreateAppointmentDto
          {
              Date = DateTime.Now.AddDays(1),
              DoctorId = "doctorId"
          };
          var patientId = "patientId";

          _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
              .ReturnsAsync((Patient)null);
          
          var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
          
          result.Should().NotBeNull();
          result.Success.Should().BeFalse();
          result.Errors.Should().Contain("Patient not found");
      }

      [Fact]
      public async Task CreateAppointmentAsync_ShouldReturnError_WhenDoctorNotFound()
      {
          var appointmentDto = new CreateAppointmentDto
          {
              Date = DateTime.Now.AddDays(1),
              DoctorId = "doctorId"
          };
          var patientId = "patientId";

          var patient = new Patient { Id = patientId };

          _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
              .ReturnsAsync(patient);

          _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
              .ReturnsAsync((Doctor)null);
          
          var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
          
          result.Should().NotBeNull();
          result.Success.Should().BeFalse();
          result.Errors.Should().Contain("Doctor not found");
      }

      [Fact]
      public async Task CreateAppointmentAsync_ShouldReturnError_WhenDoctorIsNotAvailable()
      {
          var appointmentDto = new CreateAppointmentDto
          {
              Date = DateTime.Now.AddDays(1),
              DoctorId = "doctorId"
          };
          var patientId = "patientId";

          var patient = new Patient { Id = patientId };
          var doctor = new Doctor { Id = appointmentDto.DoctorId };

          _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
              .ReturnsAsync(patient);

          _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
              .ReturnsAsync(doctor);

          _doctorRepositoryMock.Setup(x => x.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date))
              .ReturnsAsync(false);
          
          var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);

          result.Should().NotBeNull();
          result.Success.Should().BeFalse();
          result.Errors.Should().Contain("Doctor is not available");
      }
      
      [Fact]
      public async Task CreateAppointmentAsync_ShouldReturnError_WhenAppointmentDateIsInThePast()
      {
          var appointmentDto = new CreateAppointmentDto
          {
              Date = DateTime.Now.AddDays(-1), 
              DoctorId = "doctorId"
          };
          var patientId = "patientId";
          
          var patient = new Patient { Id = patientId };
          
          var doctor = new Doctor { Id = appointmentDto.DoctorId };

          _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
              .ReturnsAsync(patient);

          _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
              .ReturnsAsync(doctor);
          
          _doctorRepositoryMock.Setup(x => x.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date))
              .ReturnsAsync(true);
          
          var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
          
          result.Should().NotBeNull();
          result.Success.Should().BeFalse();
          result.Errors.Should().Contain("Appointment date has to be in the future");
      }

      
      [Fact]
      public async Task GetAppointmentsAsync_ShouldReturnSuccess_WhenAppointmentsAreFound()
        {
            var patientId = "patientId";
            var filterDto = new AppointmentFilterDto
            {
                Page = 1,
                PageSize = 10,
                FromDate = DateTime.Now.AddDays(-7),
                ToDate = DateTime.Now.AddDays(7),
                Speciality = DoctorSpecialities.Doctor
            };

            var patient = new Patient { Id = patientId };

            var filteredAppointments = new FilteredResult<Appointment>
            {
                Items = new List<Appointment>
                {
                    new Appointment
                    {
                        AppointmentDate = DateTime.Now.AddDays(1),
                        Doctor = new Doctor
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            DoctorSpeciality = DoctorSpecialities.Doctor
                        },
                        Status = AppointmentStatuses.Scheduled
                        
                    }
                },
                TotalCount = 1
            };

            _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
                .ReturnsAsync(patient);

            _appointmentRepositoryMock.Setup(x => x.GetAppointmentsByFilter(It.IsAny<AppointmentFilter>(), It.IsAny<Paging>(), patientId))
                .ReturnsAsync(filteredAppointments);

            var expectedResult = new FilteredResult<AppointmentDto>
            {
                Items = new List<AppointmentDto>
                {
                    new AppointmentDto
                    {
                        Date = DateTime.Now.AddDays(1),
                        Status = AppointmentStatuses.Scheduled
                    }
                },
                TotalCount = 1
            };

            _mapperMock.Setup(x => x.Map<FilteredResult<AppointmentDto>>(filteredAppointments))
                .Returns(expectedResult);
            
            var result = await _appointmentService.GetAppointmentsAsync(filterDto, patientId);
            
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().HaveCount(1);

            var appointmentDto = result.Data.Items.First();
            appointmentDto.Date.Should().Be(expectedResult.Items.First().Date);
        }

        [Fact]
        public async Task GetAppointmentsAsync_ShouldReturnError_WhenPatientNotFound()
        {
            var filterDto = new AppointmentFilterDto
            {
                Page = 1,
                PageSize = 10
            };
            var patientId = "patientId";

            _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
                .ReturnsAsync((Patient)null);
            
            var result = await _appointmentService.GetAppointmentsAsync(filterDto, patientId);
            
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Patient not found");
        }

     

 }
