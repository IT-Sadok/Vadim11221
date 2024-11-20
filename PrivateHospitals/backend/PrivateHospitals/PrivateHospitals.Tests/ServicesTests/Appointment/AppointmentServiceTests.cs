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
     // private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
     // private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
     // private readonly Mock<IMapper> _mapperMock;
     // private readonly Mock<IPatientRepository> _patientRepositoryMock;
     // private readonly IAppointmentService _appointmentService;
     //
     // public AppointmentServiceTests()
     // {
     //     _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
     //     _doctorRepositoryMock = new Mock<IDoctorRepository>();
     //     _patientRepositoryMock = new Mock<IPatientRepository>();
     //     _mapperMock = new Mock<IMapper>();
     //     _appointmentService = new AppointmentService(
     //         _appointmentRepositoryMock.Object,
     //         _doctorRepositoryMock.Object,
     //         _mapperMock.Object,
     //         _patientRepositoryMock.Object
     //     );
     // }
     //
     // [Fact]
     // public async Task CreateAppointmentAsync_ShouldReturnSuccess_WhenDoctorAndPatientExist()
     // {
     //     var appointmentDto = new CreateAppointmentDto()
     //     {
     //         DoctorId = "1",
     //         PatientId = "2",
     //         Date = DateTime.UtcNow
     //     };
     //
     //     var doctor = new Doctor() { Id = "1" };
     //     var patient = new Patient() { Id = "2" };
     //     var appointment = new Appointment() { AppointmentId = 1 };
     //
     //     _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
     //         .ReturnsAsync(doctor);
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(appointmentDto.PatientId))
     //         .ReturnsAsync(patient);
     //     _doctorRepositoryMock.Setup(x => x.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date))
     //         .ReturnsAsync(true);
     //     _mapperMock.Setup(x => x.Map<Appointment>(appointmentDto))
     //         .Returns(appointment);
     //     
     //     var result = await _appointmentService.CreateAppointmentAsync(appointmentDto);
     //
     //     result.Should().NotBeNull();
     //     result.Data.Should().BeTrue();
     //     result.Errors.Should().BeEmpty();
     //     _appointmentRepositoryMock.Verify(x => x.CreateAppointmentAsync(appointment), Times.Once);
     // }
     //
     // [Fact]
     // public async Task CreateAppointmentAsync_ShouldReturnError_WhenDoctorNotFound()
     // {
     //     var appointmentDto = new CreateAppointmentDto()
     //     {
     //         DoctorId = "1",
     //         PatientId = "2",
     //         Date = DateTime.UtcNow
     //     };
     //
     //     _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
     //         .ReturnsAsync((Doctor)null);
     //     
     //     var result = await _appointmentService.CreateAppointmentAsync(appointmentDto);
     //     
     //     result.Should().NotBeNull();
     //     result.Data.Should().BeFalse();
     //     result.Errors.Should().Contain("Doctor not found");
     // }
     //
     // [Fact]
     // public async Task CreateAppointmentAsync_ShouldReturnError_WhenPatientNotFound()
     // {
     //     var appointmentDto = new CreateAppointmentDto()
     //     {
     //         DoctorId = "1",
     //         PatientId = "2",
     //         Date = DateTime.UtcNow
     //     };
     //     
     //     var doctor = new Doctor() { Id = "1" };
     //
     //     _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
     //         .ReturnsAsync(doctor);
     //
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(appointmentDto.PatientId))
     //         .ReturnsAsync((Patient)null);
     //     
     //     var result = await _appointmentService.CreateAppointmentAsync(appointmentDto);
     //     
     //     result.Should().NotBeNull();
     //     result.Data.Should().BeFalse();
     //     result.Errors.Should().Contain("Patient not found");
     // }
     //
     // [Fact]
     // public async Task CreateAppointmentAsync_ShouldReturnError_WhenDoctorIsNotAvailable()
     // {
     //     var appointmentDto = new CreateAppointmentDto()
     //     {
     //         DoctorId = "1",
     //         PatientId = "2",
     //         Date = DateTime.UtcNow
     //     };
     //     
     //     var doctor = new Doctor() { Id = "1" };
     //     var patient = new Patient() { Id = "2" };
     //
     //     _doctorRepositoryMock.Setup(x => x.GetDoctorByIdAsync(appointmentDto.DoctorId))
     //         .ReturnsAsync(doctor);
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(appointmentDto.PatientId))
     //         .ReturnsAsync(patient);
     //     _doctorRepositoryMock.Setup(x => x.IsDoctorAvailableAsync(appointmentDto.DoctorId, appointmentDto.Date))
     //         .ReturnsAsync(false);
     //
     //    var result = await _appointmentService.CreateAppointmentAsync(appointmentDto);
     //    
     //    result.Should().NotBeNull();
     //    result.Data.Should().BeFalse();
     //    result.Errors.Should().Contain("Doctor is not available");
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsBySpecialityAsync_ShouldReturnAppointments()
     // {
     //     string patientId = "1";
     //     var speciality = DoctorSpecialities.Doctor;
     //     
     //     var patient = new Patient() { Id = patientId };
     //     var appointments = new List<Appointment>
     //     {
     //         new Appointment { AppointmentId = 1 }
     //     };
     //     var appointmentsDto = new List<AppointmentDto>
     //     {
     //         new AppointmentDto { Status = AppointmentStatuses.Scheduled, Date = DateTime.UtcNow}
     //     };
     //     
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     _appointmentRepositoryMock.Setup(x => x.GetAppointmentBySpecialityAsync(patientId, speciality))
     //         .ReturnsAsync(appointments);
     //     _mapperMock.Setup(x => x.Map<List<AppointmentDto>>(appointments))
     //         .Returns(appointmentsDto);
     //
     //     var result = await _appointmentService.GetAppointmentsBySpecialityAsync(patientId, speciality);
     //     
     //     result.Should().NotBeNull();
     //     result.Data.Should().BeEquivalentTo(appointmentsDto);
     //     result.Errors.Should().BeEmpty();
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsBySpecialityAsync_ShouldReturnError_WhenPatientNotFound()
     // {
     //     string patientId = "1";
     //     var speciality = DoctorSpecialities.Doctor;
     //     
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync((Patient)null);
     //     
     //     var result = await _appointmentService.GetAppointmentsBySpecialityAsync(patientId, speciality);
     //     
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("Patient not found");
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsBySpeciality_ShouldReturnError_WhenSpecialityIsNull()
     // {
     //     string patientId = "1";
     //     
     //     var patient = new Patient() { Id = patientId };
     //
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     
     //     var result = await _appointmentService.GetAppointmentsBySpecialityAsync(patientId, null);
     //
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("Doctor speciality can`t be null");
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsBySpeciality_ShouldReturnError_WhenApppointmentNotFound()
     // {
     //     string patientId = "1";
     //     var speciality = DoctorSpecialities.Doctor;
     //     
     //     var patient = new Patient() { Id = patientId };
     //     var appointments = new List<Appointment>
     //     {
     //         new Appointment { AppointmentId = 1 }
     //     };
     //     
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     _appointmentRepositoryMock.Setup(x => x.GetAppointmentBySpecialityAsync(patientId, speciality))
     //         .ReturnsAsync(new List<Appointment>());
     //     
     //     var result = await _appointmentService.GetAppointmentsBySpecialityAsync(patientId, speciality);
     //     
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("Appointment not found");
     //
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsByDateAsync_ShouldReturnAppointments()
     // {
     //     var patientId = "1";
     //     var fromDate =  new DateTime(2024, 11, 19);
     //     var toDate = new DateTime(2024, 12, 20);
     //
     //     var patient = new Patient { Id = patientId };
     //     var appointments = new List<Appointment>{ new Appointment { AppointmentId = 1 } };
     //     var appointmentsDto = new List<AppointmentDto>
     //     {
     //         new AppointmentDto { Date = DateTime.Now, Status = AppointmentStatuses.Scheduled }
     //     };
     //
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     _appointmentRepositoryMock.Setup(x => x.GetAppointmentsByDateAsync(patientId, fromDate, toDate))
     //         .ReturnsAsync(appointments);
     //     _mapperMock.Setup(x => x.Map<List<AppointmentDto>>(appointments))
     //         .Returns(appointmentsDto);
     //     
     //     var result = await _appointmentService.GetAppointmentsByDateAsync(patientId, fromDate, toDate);
     //     
     //     result.Should().NotBeNull();
     //     result.Data.Should().Contain(appointmentsDto);
     //     result.Errors.Should().BeEmpty();
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsByDateAsync_ShouldReturnError_WhenPatientNotFound()
     // {
     //     var patientId = "1";
     //     var fromDate =  new DateTime(2024, 11, 19);
     //     var toDate = new DateTime(2024, 12, 20);
     //
     //     var patient = new Patient { Id = patientId };
     //
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync((Patient)null);
     //     
     //     var result = await _appointmentService.GetAppointmentsByDateAsync(patientId, fromDate, toDate);
     //     
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("Patient not found");
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsByDateAsync_ShouldReturnError_WhenDateIsNull()
     // {
     //     var patientId = "1"; 
     //     
     //     var patient = new Patient { Id = patientId };
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     
     //     var result = await _appointmentService.GetAppointmentsByDateAsync(patientId, null, null);
     //     
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("FromDate and ToDate cannot be null");
     // }
     //
     // [Fact]
     // public async Task GetAppointmentsByDateAsync_ShouldReturnError_WhenAppointmentsNotFound()
     // {
     //     var patientId = "1";
     //     var fromDate =  new DateTime(2024, 11, 19);
     //     var toDate = new DateTime(2024, 12, 20);
     //
     //     var patient = new Patient { Id = patientId };
     //     var appointments = new List<Appointment>{ };
     //     var appointmentsDto = new List<AppointmentDto> { };
     //
     //     _patientRepositoryMock.Setup(x => x.GetPatientByIdAsync(patientId))
     //         .ReturnsAsync(patient);
     //     _appointmentRepositoryMock.Setup(x => x.GetAppointmentsByDateAsync(patientId, fromDate, toDate))
     //         .ReturnsAsync(appointments);
     //     _mapperMock.Setup(x => x.Map<List<AppointmentDto>>(appointments))
     //         .Returns(appointmentsDto);
     //     
     //     var result = await _appointmentService.GetAppointmentsByDateAsync(patientId, fromDate, toDate);
     //     
     //     result.Should().NotBeNull();
     //     result.Errors.Should().Contain("Appointment not found");
     // }

 }
