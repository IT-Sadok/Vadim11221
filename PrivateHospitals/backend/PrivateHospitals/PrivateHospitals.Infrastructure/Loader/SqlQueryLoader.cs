using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Infrastructure.Loader
{
    public class SqlQueryLoader
    {
        private readonly ResourceManager _resourceManager;

        public SqlQueryLoader()
        {
            _resourceManager = new ResourceManager("PrivateHospitals.Infrastructure.Resources.SqlQueries", typeof(SqlQueryLoader).Assembly);
        }

        public string GetQuery(string key)
        {
            var query = _resourceManager.GetString(key);

            if (string.IsNullOrWhiteSpace(query))
            {
                throw new KeyNotFoundException($"Sql query not found for key: {key}");
            }

            return query;   
        }
    }
}
