using PrivateHospitals.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Interfaces.DoctorInfoInterface
{
    public interface IDoctorInfoRepository
    {
        Task<bool> UpsertDoctorInfo(DoctorInfo doctorInfo);
    }
}
