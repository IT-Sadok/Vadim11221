using Dapper;
using PrivateHospitals.Core.Models.Statistics;
using PrivateHospitals.Infrastructure.Interfaces.Statistics;
using PrivateHospitals.Infrastructure.ResourcesSql;
using System.Data;


namespace PrivateHospitals.Infrastructure.Repositories.Statistics
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IDbConnection _dbConnection;
        public StatisticsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<DoctorAppointments>> GetDoctorAppointmentsMoreOneAsync()
        {
            return await _dbConnection.QueryAsync<DoctorAppointments>(SqlQueries.DoctorAppointmentsMoreThanOne);
        }

        public async Task<IEnumerable<DoctorAvgAppointments>> GetDoctorAvgAppointmentsAsync()
        {
            return await _dbConnection.QueryAsync<DoctorAvgAppointments>(SqlQueries.DoctorAvgAppointments);
        }

        public async Task<IEnumerable<DoctorSpecialityCount>> GetDoctorCountBySpesialityAsync()
        {
            return await _dbConnection.QueryAsync<DoctorSpecialityCount>(SqlQueries.DoctorCountBySpeciality);
        }

        public async Task<IEnumerable<DoctorYearsOfExperience>> GetDoctorYearsOfExperienceAsync()
        {
            return await _dbConnection.QueryAsync<DoctorYearsOfExperience>(SqlQueries.DoctorYearsOfExperience);
        }

        public async Task<IEnumerable<DoctorQuantiles>> GetDoctorYearsQUantiliesASync()
        {
            return await _dbConnection.QueryAsync<DoctorQuantiles>(SqlQueries.DoctorQuantiles);
        }
    }
}
