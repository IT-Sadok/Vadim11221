using AutoMapper;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Dtos.User;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Application.Profiles;

public class MapProfile: Profile
{
    public MapProfile()
    {
            CreateMap<RegisterDto, Doctor>().ReverseMap();
            CreateMap<RegisterDto, Patient>().ReverseMap();
            CreateMap<AppUser, Doctor>().ReverseMap();
            CreateMap<CreateAppointmentDto, Appointment>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
    }
}