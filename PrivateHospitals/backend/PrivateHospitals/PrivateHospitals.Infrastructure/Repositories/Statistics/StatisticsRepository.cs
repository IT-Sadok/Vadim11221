using Dapper;
using PrivateHospitals.Core.Models.Statistics;
using PrivateHospitals.Infrastructure.Interfaces.Statistics;
using PrivateHospitals.Infrastructure.Loader;
using System.Data;


namespace PrivateHospitals.Infrastructure.Repositories.Statistics
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly SqlQueryLoader _queryLoader;

        public StatisticsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _queryLoader = new SqlQueryLoader();
        }

        public async Task<IEnumerable<DoctorAppointments>> GetDoctorAppointmentsMoreOneAsync()
        {
            var query = _queryLoader.GetQuery("DoctorAppointmentsMoreThanOne");

            return await _dbConnection.QueryAsync<DoctorAppointments>(query);
        }

        public async Task<IEnumerable<DoctorAvgAppointments>> GetDoctorAvgAppointmentsAsync()
        {
            var query = _queryLoader.GetQuery("DoctorAvgAppointments");

            return await _dbConnection.QueryAsync<DoctorAvgAppointments>(query);
        }

        public async Task<IEnumerable<DoctorSpecialityCount>> GetDoctorCountBySpesialityAsync()
        {
            var query = _queryLoader.GetQuery("DoctorCountBySpeciality");                                                                                                                                                                                                                                                                                                                                                   

            return await _dbConnection.QueryAsync<DoctorSpecialityCount>(query);
        }

        public async Task<IEnumerable<DoctorYearsOfExperience>> GetDoctorYearsOfExperienceAsync()
        {
            var query = _queryLoader.GetQuery("DoctorYearsOfExperience");

            return await _dbConnection.QueryAsync<DoctorYearsOfExperience>(query);
        }

        public async Task<IEnumerable<DoctorQuantiles>> GetDoctorYearsQUantiliesASync()
        {
            var query = _queryLoader.GetQuery("DoctorQuantiles");

            return await _dbConnection.QueryAsync<DoctorQuantiles>(query);
        }
    }
}
