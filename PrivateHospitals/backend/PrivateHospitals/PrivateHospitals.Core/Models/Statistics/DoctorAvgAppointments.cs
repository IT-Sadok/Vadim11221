using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Core.Models.Statistics
{
    public class DoctorAvgAppointments
    {
        public int DoctorSpeciality { get; set; }
        public decimal AvgAppointments { get; set; }
    }
}
