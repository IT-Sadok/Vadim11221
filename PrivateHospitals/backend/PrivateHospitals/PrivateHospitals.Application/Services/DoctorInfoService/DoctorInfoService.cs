using AutoMapper;
using PrivateHospitals.Application.Dtos.DoctorInfo;
using PrivateHospitals.Application.Interfaces.DoctorInfo;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.DoctorInfoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Services.DoctorInfoService
{
    public class DoctorInfoService : IDoctorInfoService
    {
        private readonly IDoctorInfoRepository _doctorInfoRepository;
        private readonly IMapper _mapper;

        public DoctorInfoService(IDoctorInfoRepository doctorInfoRepository, IMapper mapper)
        {
            _doctorInfoRepository = doctorInfoRepository;
            _mapper = mapper;
        }

        public async Task<bool> UpsertDoctorInfoAsync(DoctorInfoDto doctorInfo)
        {
            if(doctorInfo is null)
            {
                return false;
            }

            var doctorInfoDto = _mapper.Map<DoctorInfo>(doctorInfo);

            return await _doctorInfoRepository.UpsertDoctorInfo(doctorInfoDto);
        }
    }
}
