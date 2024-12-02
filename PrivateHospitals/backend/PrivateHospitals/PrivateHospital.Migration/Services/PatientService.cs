using PrivateHospital.Migration.Dto;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospital.Migration.Interfaces;
using PrivateHospitals.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration.Services
{
    public class PatientService(PatientRepository _patientRepository)
    {
        public async Task SavePatientAsync(PatientDto patientDto)
        {
            if(patientDto != null)
            {
                var patient = await _patientRepository.GetByExternalId(patientDto.ExternalId);

                if(patient == null)
                {
                    patient = new Patient
                    {
                        Id = patientDto.PatientId,
                        CompanyId = patientDto.CompanyId,   
                        ExternalId = patientDto.ExternalId,
                        UserName = patientDto.UserName,
                        FirstName = patientDto.FirstName,
                        LastName = patientDto.LastName
                    };

                    await _patientRepository.AddAsync(patient);
                }
            }
        }
    }
}
