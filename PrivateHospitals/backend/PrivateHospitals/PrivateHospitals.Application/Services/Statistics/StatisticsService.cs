using PrivateHospitals.Application.Interfaces.Statistics;
using PrivateHospitals.Core.Models.Statistics;
using PrivateHospitals.Infrastructure.Interfaces.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IEnumerable<DoctorAppointments>> GetDoctorAppointmentsMoreOneAsync()
        {
            return await _statisticsRepository.GetDoctorAppointmentsMoreOneAsync();
        }

        public async Task<IEnumerable<DoctorAvgAppointments>> GetDoctorAvgAppointmentsAsync()
        {
            return await _statisticsRepository.GetDoctorAvgAppointmentsAsync();
        }

        public async Task<IEnumerable<DoctorSpecialityCount>> GetDoctorCountBySpecialityAsync()
        {
            return await _statisticsRepository.GetDoctorCountBySpesialityAsync();
        }

        public async Task<IEnumerable<DoctorYearsOfExperience>> GetDoctorYearsOfExperiencesAsync()
        {
            return await _statisticsRepository.GetDoctorYearsOfExperienceAsync();
        }

        public async Task<IEnumerable<DoctorQuantiles>> GetDoctorYearsQuantilesAsync()
        {
            return await _statisticsRepository.GetDoctorYearsQUantiliesASync();
        }
    }
}
