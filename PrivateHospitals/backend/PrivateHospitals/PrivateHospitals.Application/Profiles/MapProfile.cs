using AutoMapper;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Profiles;

public class MapProfile: Profile
{
    public MapProfile()
    {
            CreateMap<RegisterDoctorDto, Doctor>().ReverseMap();
            CreateMap<RegisterPatientDto, Patient>().ReverseMap();
    }
}