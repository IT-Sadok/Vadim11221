using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models.Users;

public class Doctor: AppUser
{
    public Doctor()
    {
        Role = Roles.Doctor;
    }
    
    public DoctorSpecialities DoctorSpeciality { get; set; }
    public double YearsOfExperience { get; set; } = 1;

    public List<MedicalCard> PatientsMedicalCards{ get; set; }
    
    public List<Appointment>? Appointments { get; set; }

    public List<WorkingHours>? WorkingHours { get; set; }
}