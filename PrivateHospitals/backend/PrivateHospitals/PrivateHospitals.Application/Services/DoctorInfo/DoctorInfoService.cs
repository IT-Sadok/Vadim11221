using PrivateHospitals.Application.Dtos.DoctorInfo;
using PrivateHospitals.Application.Interfaces.DoctorInfo;
using PrivateHospitals.Infrastructure.Interfaces.DoctorInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Services.DoctorInfo
{
    public class DoctorInfoService : IDoctorInfoService
    {
        private readonly IDoctorInfoRepository _doctorInfoRepository;

        public DoctorInfoService(IDoctorInfoRepository doctorInfoRepository)
        {
            _doctorInfoRepository = doctorInfoRepository;
        }

        public async Task<bool> UpsertDoctorInfoAsync(DoctorInfoDto doctorInfo)
        {
            if(doctorInfo is null)
            {
                return false;
            }

            return await _doctorInfoRepository.UpsertDoctorInfo(doctorInfo.DoctorInfoId,
             doctorInfo.FirstName,
             doctorInfo.LastName,
             doctorInfo.Email,
             doctorInfo.University,
             doctorInfo.INN,
             doctorInfo.DiplomNumber);
        }
    }
}
