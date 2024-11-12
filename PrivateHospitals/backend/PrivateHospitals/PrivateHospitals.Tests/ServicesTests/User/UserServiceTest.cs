using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Application.Interfaces.Token;
using PrivateHospitals.Application.Services.User;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.User;
using Xunit;
using Assert = Xunit.Assert;

namespace PrivateHospitals.Tests.ServicesTests.User;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _mapperMock = new Mock<IMapper>();

        var userManagerMock = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);

        _signInManagerMock = new Mock<SignInManager<AppUser>>(
            userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
            null, null, null, null);

        _userService = new UserService(
            _userRepositoryMock.Object,
            _signInManagerMock.Object,
            _tokenServiceMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnSuccess_WhenDoctorCreatedSuccessfully()
    {
        var registerDto = new RegisterDto()
        {
            FirstName = "Joe",
            LastName = "Doctor",
            UserName = "JoeDoctor",
            Email = "joe.doctor@gmail.com",
            Role = Roles.Doctor,
            Password = "Doctor112281_"
        };

        var doctor = new Doctor();
        _mapperMock.Setup(x => x.Map<Doctor>(registerDto)).Returns(doctor);
        _userRepositoryMock.Setup(x => x.CreateUserAsync(doctor, registerDto.Password))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _userService.RegisterUser(registerDto);

        Assert.True(result.Success);
        _userRepositoryMock.Verify(x => x.CreateUserAsync(doctor, registerDto.Password), Times.Once);
        _userRepositoryMock.Verify(x => x.AddUserToRoleAsync(doctor, Roles.Doctor), Times.Once);
    }
    
    [Fact]
    public async Task RegisterUser_ShouldReturnSuccess_WhenPatientCreatedSuccessfully()
    {
        var registerDto = new RegisterDto()
        {
            FirstName = "Joe",
            LastName = "Patient",
            UserName = "JoePatient",
            Email = "joe.patient@gmail.com",
            Role = Roles.Patient,
            Password = "Patient112281_"
        };

        var patient = new Patient();
        _mapperMock.Setup(x => x.Map<Patient>(registerDto)).Returns(patient);
        _userRepositoryMock.Setup(x => x.CreateUserAsync(patient, registerDto.Password))
            .ReturnsAsync(IdentityResult.Success);
        _userRepositoryMock.Setup(x => x.AddUserToRoleAsync(patient, Roles.Patient)).Returns(Task.FromResult(IdentityResult.Success));

        var result = await _userService.RegisterUser(registerDto);

        Assert.True(result.Success);
        _userRepositoryMock.Verify(x => x.CreateUserAsync(patient, registerDto.Password), Times.Once);
        _userRepositoryMock.Verify(x => x.AddUserToRoleAsync(patient, Roles.Patient), Times.Once);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnError_WhenDoctorCreationFails()
    {
        var registerDto = new RegisterDto()
        {
            FirstName = "Joe",
            LastName = "Doctor",
            UserName = "JoeDoctor",
            Email = "joe.doctor@gmail.com",
            Role = Roles.Doctor,
            Password = "Doctor112281_"
        };

        var doctor = new Doctor();
        _mapperMock.Setup(x => x.Map<Doctor>(registerDto)).Returns(doctor);
        _userRepositoryMock.Setup(x => x.CreateUserAsync(doctor, registerDto.Password))
            .ReturnsAsync(IdentityResult.Failed());
        
        var result = await _userService.RegisterUser(registerDto);
        
        Assert.False(result.Success);
        Assert.Contains("Something went wrong during creating the doctor.", result.Errors);
    }
    
    [Fact]
    public async Task LoginUserAsync_ShouldReturnSuccess_WhenLoginIsSuccessful()
    {
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password123_" };
        var user = new AppUser { Email = loginDto.Email };
        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(loginDto.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, loginDto.Password, false))
            .ReturnsAsync(SignInResult.Success);
        _tokenServiceMock.Setup(x => x.CreateToken(user)).Returns("test-token");
        
        var result = await _userService.LoginUserAsync(loginDto);
        
        Assert.True(result.Success);
        Assert.Equal(loginDto.Email, result.Data.Email);
        Assert.Equal("test-token", result.Data.Token);
    }
    
    [Fact]
    public async Task LoginUserAsync_ShouldReturnError_WhenUserNotFound()
    {
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password123_" };
        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(loginDto.Email)).ReturnsAsync((AppUser)null);
        
        var result = await _userService.LoginUserAsync(loginDto);
        
        Assert.False(result.Success);
        Assert.Contains("User not found", result.Errors);
    }
    [Fact]
    public async Task LoginUserAsync_ShouldReturnError_WhenPasswordIsInvalid()
    {
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password123_" };
        var user = new AppUser { Email = loginDto.Email };
        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(loginDto.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginDto.Password, false))
            .ReturnsAsync(SignInResult.Failed);

        var result = await _userService.LoginUserAsync(loginDto);
        
        Assert.False(result.Success);
        Assert.Contains("Invalid login or password", result.Errors);
    }
}