using PrivateHospital.Migration.Dto;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospital.Migration.Interfaces;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration.Services
{
    public class DoctorService(DoctorRepository _doctorRepository)
    {
        public async Task SaveDoctor(DoctorDto doctorDto)
        {
            if(doctorDto != null)
            {
                var doctor = await _doctorRepository.GetByExternalId(doctorDto.ExternalId);

                if (doctor == null)
                {
                    doctor = new Doctor
                    {
                        Id = doctorDto.DoctorId,
                        CompanyId = doctorDto.CompanyId, 
                        ExternalId = doctorDto.ExternalId,
                        UserName = doctorDto.UserName,
                        FirstName = doctorDto.FirstName,
                        LastName = doctorDto.LastName,
                        DoctorSpeciality = doctorDto.Speciality
                    };
                }

                await _doctorRepository.AddAsync(doctor);
                
            }
        }
    }
}
