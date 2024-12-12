using Dapper;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Interfaces.DoctorInfoInterface;
using PrivateHospitals.Infrastructure.ResourcesSql;
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

        public DoctorInfoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> UpsertDoctorInfo(DoctorInfo doctorInfo)
        {
            var existDoctor = await _dbConnection.QueryFirstAsync<bool>(SqlQueries.DoctorExists, new { doctorInfo.DoctorInfoId });

            if (!existDoctor)
            {
                var insertQuery = SqlQueries.InsertDoctorInfo;
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
                var updateQuery = SqlQueries.UpdateDoctorInfo;
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
