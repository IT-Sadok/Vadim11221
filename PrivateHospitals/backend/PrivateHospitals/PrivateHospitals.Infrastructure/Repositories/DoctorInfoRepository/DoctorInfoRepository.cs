using Dapper;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.DoctorInfoInterface;
using PrivateHospitals.Infrastructure.Loader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Repositories.DoctorInfoRepository
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

        public async Task<bool> UpsertDoctorInfo(DoctorInfo doctorInfo)
        {
            var doctorExistQuery = _queryLoader.GetQuery("DoctorExists");
            var existDoctor = await _dbConnection.QueryFirstAsync<bool>(doctorExistQuery, new { doctorInfo.DoctorInfoId });

            if (!existDoctor)
            {
                var insertQuery = _queryLoader.GetQuery("InsertDoctorInfo");
                await _dbConnection.ExecuteAsync(insertQuery, new
                {
                    DoctorId = doctorInfo.DoctorInfoId,
                    FirstName = doctorInfo.FirstName,
                    LastName = doctorInfo.LastName,
                    Email = doctorInfo.Email,
                    Universities = doctorInfo.University,
                    INN = doctorInfo.INN,
                    DiplomNumber = doctorInfo.DiplomNumber
                });

                return true;
            }
            else
            {
                var updateQuery = _queryLoader.GetQuery("UpdateDoctorInfo");
                await _dbConnection.ExecuteAsync(updateQuery, new
                {
                    DoctorId = doctorInfo.DoctorInfoId,
                    FirstName = doctorInfo.FirstName,
                    LastName = doctorInfo.LastName,
                    Email = doctorInfo.Email,
                    Universities = doctorInfo.University,
                    INN = doctorInfo.INN,
                    DiplomNumber = doctorInfo.DiplomNumber
                });

                return true;
            }
        }

    }
}
