using PrivateHospitals.Core.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Interfaces.Statistics
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<DoctorSpecialityCount>> GetDoctorCountBySpesialityAsync();
        Task<IEnumerable<DoctorYearsOfExperience>> GetDoctorYearsOfExperienceAsync();
        Task<IEnumerable<DoctorQuantiles>> GetDoctorYearsQUantiliesASync();
        Task<IEnumerable<DoctorAvgAppointments>> GetDoctorAvgAppointmentsAsync();
        Task<IEnumerable<DoctorAppointments>> GetDoctorAppointmentsMoreOneAsync();

    }
}
