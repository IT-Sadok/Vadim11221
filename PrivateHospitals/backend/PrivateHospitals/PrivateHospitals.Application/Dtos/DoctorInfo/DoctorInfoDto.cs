using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Dtos.DoctorInfo
{
    public record DoctorInfoDto
    {
        public string DoctorInfoId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string INN { get; init; }
        public string DiplomNumber { get; init; }
        public string University { get; init; }
    }
}
