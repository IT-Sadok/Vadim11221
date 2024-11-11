using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Interfaces;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Data.Interfaces.User;
using PrivateHospitals.Application.Services.User;
using PrivateHospitals.Core.Enum;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly UserService _userService;

    public UserServiceTest()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        _signInManagerMock = new Mock<SignInManager<AppUser>>(
            _userManagerMock.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<AppUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<AppUser>>().Object
        );

        _userService = new UserService(
            _userRepositoryMock.Object,
            _signInManagerMock.Object,
            _tokenServiceMock.Object,
            _mapperMock.Object);
    }
    
    [Fact]
    public async Task RegisterDoctorAsync_ShouldReturnSuccess_WhenDoctorIsCreated()
    {
        var registerDoctorDto = new RegisterDoctorDto
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe",
            Email = "john@example.com",
            Speciality = SpecialitiesOfDoctor.Cardiologist,
            Password = "password123"
        };
        
        var doctor = new Doctor();
        _mapperMock.Setup(m => m.Map<Doctor>(registerDoctorDto)).Returns(doctor);
        _userRepositoryMock.Setup(r => r.CreateUserAsync(doctor, registerDoctorDto.Password))
                           .ReturnsAsync(IdentityResult.Success);
        _userRepositoryMock.Setup(r => r.AddUserToRoleAsync(doctor, Roles.Doctor))
                           .ReturnsAsync(IdentityResult.Success);

        var result = await _userService.RegisterDoctorAsync(registerDoctorDto);

        result.Success.Should().BeTrue();
        result.Data.Should().BeTrue();
    }
    
    [Fact]
    public async Task RegisterDoctorAsync_ShouldReturnError_WhenDoctorIsNullAfterMapping()
    {
        var registerDoctorDto = new RegisterDoctorDto
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe",
            Email = "john@example.com",
            Speciality = SpecialitiesOfDoctor.Cardiologist,
            Password = "password123"
        };
        
        _mapperMock.Setup(m => m.Map<Doctor>(registerDoctorDto)).Returns((Doctor)null);

        var result = await _userService.RegisterDoctorAsync(registerDoctorDto);

        result.Success.Should().BeFalse();
        result.Errors.Should().Contain("Doctor is null");
    }
    
    [Fact]
    public async Task RegisterPatientAsync_ShouldReturnSuccess_WhenPatientIsCreated()
    {
        var registerPatientDto = new RegisterPatientDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "jane@example.com",
            Password = "password123"
        };
        
        var patient = new Patient();
        _mapperMock.Setup(m => m.Map<Patient>(registerPatientDto)).Returns(patient);
        _userRepositoryMock.Setup(r => r.CreateUserAsync(patient, registerPatientDto.Password))
                           .ReturnsAsync(IdentityResult.Success);
        _userRepositoryMock.Setup(r => r.AddUserToRoleAsync(patient, Roles.Patient))
                           .ReturnsAsync(IdentityResult.Success);

        var result = await _userService.RegisterPatientAsync(registerPatientDto);

        result.Success.Should().BeTrue();
        result.Data.Should().BeTrue();
    }
    
    [Fact]
    public async Task RegisterPatientAsync_ShouldReturnError_WhenPatientIsNullAfterMapping()
    {
        var registerPatientDto = new RegisterPatientDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "jane@example.com",
            Password = "password123"
        };
        
        _mapperMock.Setup(m => m.Map<Patient>(registerPatientDto)).Returns((Patient)null);

        var result = await _userService.RegisterPatientAsync(registerPatientDto);

        result.Success.Should().BeFalse();
        result.Errors.Should().Contain("Patient is null");
    }
    
    [Fact]
    public async Task TestUserLoginAsync_ShouldReturnTrue_WhenLoginIsSuccessful()
    {
        var user = new AppUser { UserName = "testuser", Email = "testuser@example.com" };
        var password = "Password123!";

        _userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, password, false, false))
                          .ReturnsAsync(SignInResult.Success);

        var result = await _signInManagerMock.Object.PasswordSignInAsync(user.UserName, password, false, false);

        result.Succeeded.Should().BeTrue();
    }
    
    [Fact]
    public async Task TestUserLoginAsync_ShouldReturnFalse_WhenLoginFails()
    {
        var user = new AppUser { UserName = "testuser", Email = "testuser@example.com" };
        var password = "IncorrectPassword";

        _userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, password, false, false))
                          .ReturnsAsync(SignInResult.Failed);

        var result = await _signInManagerMock.Object.PasswordSignInAsync(user.UserName, password, false, false);

        result.Succeeded.Should().BeFalse();
    }
    
    [Fact]
    public async Task TestUserLoginAsync_ShouldReturnFalse_WhenUserNotFound()
    {   
        var userName = "nonexistentuser";
        var password = "AnyPassword";

        _userManagerMock.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync((AppUser)null);
        
        var user = await _userManagerMock.Object.FindByNameAsync(userName);
        
        user.Should().BeNull();
    }
}
