using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration.Interfaces
{
    public interface IIdRepository<T> where T : class
    {
        Task<T> GetByExternalId(string externalId);
    }
}
