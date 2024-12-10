using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Interfaces.DoctorInfo
{
    public interface IDoctorInfoRepository
    {
        Task<bool> UpsertDoctorInfo(string doctorId, string firstName, string lastName, string email, string university, string INN, string diplomNumber);
    }
}
