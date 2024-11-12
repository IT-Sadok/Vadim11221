using AutoMapper;
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
    }
}