using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models.Users;

public class Doctor: AppUser
{
    public Doctor()
    {
        Role = Roles.Doctor;
    }
    
    public DoctorSpecialities DoctorSpeciality { get; set; }
    
    public List<MedicalCard> PatientsMedicalCards{ get; set; }
    
    public List<Appointment>? Appointmants { get; set; }
    
    public string WorkingHoursJson { get; set; }
}