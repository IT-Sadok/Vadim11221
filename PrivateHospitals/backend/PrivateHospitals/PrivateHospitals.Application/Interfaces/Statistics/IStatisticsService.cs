using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Interfaces.Statistics
{
    public interface IStatisticsService
    {
        Task<IEnumerable<DoctorSpecialityCount>> GetDoctorCountBySpecialityAsync();
        Task<IEnumerable<DoctorYearsOfExperience>> GetDoctorYearsOfExperiencesAsync();
        Task<IEnumerable<DoctorQuantiles>> GetDoctorYearsQuantilesAsync();
        Task<IEnumerable<DoctorAvgAppointments>> GetDoctorAvgAppointmentsAsync();
        Task<IEnumerable<DoctorAppointments>> GetDoctorAppointmentsMoreOneAsync();
    }
}
