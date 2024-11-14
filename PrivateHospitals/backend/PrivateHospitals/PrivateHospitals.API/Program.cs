using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrivateHospitals.API.Middleware;
using PrivateHospitals.Application.Interfaces.Appointment;
using PrivateHospitals.Application.Interfaces.Token;
using PrivateHospitals.Application.Interfaces.User;
using PrivateHospitals.Application.Profiles;
using PrivateHospitals.Application.Services.Appointment;
using PrivateHospitals.Application.Services.Token;
using PrivateHospitals.Application.Services.User;
using PrivateHospitals.Application.Validators.User;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Appointment;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using PrivateHospitals.Infrastructure.Interfaces.Patient;
using PrivateHospitals.Infrastructure.Interfaces.User;
using PrivateHospitals.Infrastructure.Repositories.Appointment;
using PrivateHospitals.Infrastructure.Repositories.Doctor;
using PrivateHospitals.Infrastructure.Repositories.Patient;
using PrivateHospitals.Infrastructure.Repositories.User;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>());;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HospitalDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<HospitalDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
                options.DefaultScheme =
                    options.DefaultSignInScheme =
                        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();



builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

app.Run();