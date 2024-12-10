using PrivateHospitals.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Core.Models.Users
{
    public class DoctorInfo 
    {
        [Key]
        public string DoctorInfoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string INN { get; set; }
        public string DiplomNumber { get; set; }
        public DoctorSpecialities? DoctorSpeciality { get; set; } = DoctorSpecialities.Doctor;
        public double? YearsOfExperience { get; set; } = 1;

        public List<MedicalCard>? PatientsMedicalCards { get; set; }

        public List<Appointment>? Appointments { get; set; }

        public List<WorkingHours>? WorkingHours { get; set; }
        public string University { get; set; }
    }
}
