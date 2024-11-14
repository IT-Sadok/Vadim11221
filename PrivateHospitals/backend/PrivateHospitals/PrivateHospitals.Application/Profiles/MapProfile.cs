using AutoMapper;
using PrivateHospitals.Application.Dtos.Appointment;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
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
            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<PatientDto, Patient>().ReverseMap();
    }
}