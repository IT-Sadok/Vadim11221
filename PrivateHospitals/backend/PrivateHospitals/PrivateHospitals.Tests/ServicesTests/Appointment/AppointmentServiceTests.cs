// using AutoMapper;
// using Moq;
// using PrivateHospitals.Application.Dtos.Appointment;
// using PrivateHospitals.Application.Services.Appointment;
// using PrivateHospitals.Core.Enum;
// using PrivateHospitals.Core.Models;
// using PrivateHospitals.Core.Models.Users;
// using PrivateHospitals.Infrastructure.Interfaces.Appointment;
// using PrivateHospitals.Infrastructure.Interfaces.Doctor;
// using PrivateHospitals.Infrastructure.Interfaces.Patient;
// using Xunit;
// using Assert = Xunit.Assert;
//
// public class AppointmentServiceTests
// {
//     private readonly Mock<IAppointmentRepository> _appointmentRepoMock;
//     private readonly Mock<IDoctorRepository> _doctorRepoMock;
//     private readonly Mock<IPatientRepository> _patientRepoMock;
//     private readonly Mock<IMapper> _mapperMock;
//     private readonly AppointmentService _appointmentService;
//
//     public AppointmentServiceTests()
//     {
//         _appointmentRepoMock = new Mock<IAppointmentRepository>();
//         _doctorRepoMock = new Mock<IDoctorRepository>();
//         _patientRepoMock = new Mock<IPatientRepository>();
//         _mapperMock = new Mock<IMapper>();
//
//         _appointmentService = new AppointmentService(
//             _appointmentRepoMock.Object,
//             _doctorRepoMock.Object,
//             _mapperMock.Object,
//             _patientRepoMock.Object
//         );
//     }
//
//     [Fact]
//     public async Task CreateAppointment_ShouldReturnSuccess_WhenValidDataProvided()
//     {
//         var createDto = new CreateAppointmentDto
//         {
//             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
//             Time = TimeSpan.FromHours(10),
//             PatientId = "patient123",
//             DoctorFirstName = "John",
//             DoctorLastName = "Doe"
//         };
//
//         var doctor = new AppUser
//         {
//             Id = "doctor123",
//             FirstName = "John",
//             LastName = "Doe"
//         };
//
//         _doctorRepoMock
//             .Setup(repo => repo.GetDoctorByFullName("John", "Doe"))
//             .ReturnsAsync(doctor);
//
//         _doctorRepoMock
//             .Setup(repo => repo.IsDoctorOnWork(It.IsAny<DateOnly>(), It.IsAny<TimeSpan>(), "doctor123"))
//             .ReturnsAsync(true);
//
//         _appointmentRepoMock
//             .Setup(repo => repo.IsDoctorHaveAppointment(It.IsAny<DateOnly>(), It.IsAny<TimeSpan>(), "doctor123"))
//             .ReturnsAsync(false);
//
//         _mapperMock
//             .Setup(mapper => mapper.Map<Appointment>(createDto))
//             .Returns(new Appointment { DoctorId = doctor.Id });
//
//         _appointmentRepoMock
//             .Setup(repo => repo.CreateAppointmentAsync(It.IsAny<Appointment>()))
//             .ReturnsAsync(true);
//
//         var result = await _appointmentService.CreateAppointment(createDto);
//         
//         Assert.True(result.Success);
//         Assert.True(result.Data);
//     }
//
//     [Fact]
//     public async Task CreateAppointment_ShouldReturnError_WhenDoctorNotFound()
//     {
//         var createDto = new CreateAppointmentDto
//         {
//             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
//             Time = TimeSpan.FromHours(10),
//             PatientId = "patient123",
//             DoctorFirstName = "Unknown",
//             DoctorLastName = "Doctor"
//         };
//
//         _doctorRepoMock
//             .Setup(repo => repo.GetDoctorByFullName("Unknown", "Doctor"))
//             .ReturnsAsync((AppUser)null);
//         
//         var result = await _appointmentService.CreateAppointment(createDto);
//         
//         Assert.False(result.Success);
//         Assert.Contains("Doctor not found", result.Errors);
//     }
//
//     [Fact]
//     public async Task GetAppointmentsBySpecialityId_ShouldReturnAppointments_WhenValidDataProvided()
//     {
//         var speciality = DoctorSpecialities.Surgeon;
//         var patientId = "patient123";
//
//         var appointments = new List<Appointment>
//         {
//             new Appointment { Date = DateOnly.FromDateTime(DateTime.Now), Time = TimeSpan.FromHours(10) }
//         };
//
//         _patientRepoMock
//             .Setup(repo => repo.GetPatientByIdAsync(patientId))
//             .ReturnsAsync(new Patient { Id = patientId });
//
//         _appointmentRepoMock
//             .Setup(repo => repo.GetAppointmentsBySpeciality(speciality, patientId))
//             .ReturnsAsync(appointments);
//
//         _mapperMock
//             .Setup(mapper => mapper.Map<List<AppointmentDto>>(appointments))
//             .Returns(new List<AppointmentDto>
//             {
//                 new AppointmentDto { Date = DateOnly.FromDateTime(DateTime.Now), Time = TimeSpan.FromHours(10), Status = AppointmentStatuses.Scheduled }
//             });
//         
//         var result = await _appointmentService.GetAppointmentsBySpecialityId(speciality, patientId);
//         
//         Assert.True(result.Success);
//         Assert.NotNull(result.Data);
//         Assert.Single(result.Data);
//     }
//
//     [Fact]
//     public async Task GetAppointmentByDate_ShouldReturnError_WhenFromDateIsGreaterThanToDate()
//     {
//         var fromDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
//         var toDate = DateOnly.FromDateTime(DateTime.Now);
//         
//         var result = await _appointmentService.GetAppointmentByDate(fromDate, toDate, "patient123");
//         
//         Assert.False(result.Success);
//         Assert.Contains("From date cannot be before to date", result.Errors);
//     }
//
//     [Fact]
//     public async Task GetAppointmentByDate_ShouldReturnError_WhenPatientNotFound()
//     {
//         var fromDate = DateOnly.FromDateTime(DateTime.Now);
//         var toDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
//
//         _patientRepoMock
//             .Setup(repo => repo.GetPatientByIdAsync("invalid_patient"))
//             .ReturnsAsync((Patient)null);
//         
//         var result = await _appointmentService.GetAppointmentByDate(fromDate, toDate, "invalid_patient");
//         
//         Assert.False(result.Success);
//         Assert.Contains("Patient not found", result.Errors);
//     }
// }
