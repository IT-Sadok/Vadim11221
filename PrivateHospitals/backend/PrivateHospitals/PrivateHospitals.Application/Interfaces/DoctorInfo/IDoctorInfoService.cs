using PrivateHospitals.Application.Dtos.DoctorInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Interfaces.DoctorInfo
{
    public interface IDoctorInfoService
    {
        Task<bool> UpsertDoctorInfoAsync(DoctorInfoDto doctorInfo);
    }
}
