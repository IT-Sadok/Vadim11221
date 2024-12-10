using Dapper;
using PrivateHospitals.Infrastructure.Interfaces.DoctorInfo;
using PrivateHospitals.Infrastructure.Loader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Repositories.DoctorInfo
{
    public class DoctorInfoRepository : IDoctorInfoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly SqlQueryLoader _queryLoader;

        public DoctorInfoRepository(IDbConnection dbConnection, SqlQueryLoader queryLoader)
        {
            _dbConnection = dbConnection;
            _queryLoader = queryLoader;
        }

        public async Task<bool> UpsertDoctorInfo(string doctorId, string firstName, string lastName, string email, string university, string INN, string diplomNumber)
        {
            var doctorExistQuery = _queryLoader.GetQuery("DoctorExists");
            var existDoctor = await _dbConnection.QueryFirstAsync<bool>(doctorExistQuery, new { doctorId });

            if (!existDoctor)
            {
                var insertQuery = _queryLoader.GetQuery("InsertDoctorInfo");
                await _dbConnection.ExecuteAsync(insertQuery, new
                {
                    DoctorId = doctorId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Universities = university,
                    INN = INN,
                    DiplomNumber = diplomNumber
                });

                return true;
            }
            else
            {
                var updateQuery = _queryLoader.GetQuery("UpdateDoctorInfo");
                await _dbConnection.ExecuteAsync(updateQuery, new
                {
                    DoctorId = doctorId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Universities = university,
                    INN = INN,
                    DiplomNumber = diplomNumber
                });

                return true;
            }
        }

    }
}
