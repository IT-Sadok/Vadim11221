using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models.Users;

public class Doctor: AppUser
{
    public Doctor()
    {
        Role = Roles.Doctor;
    }
    
    public DoctorSpecialities DoctorSpeciality { get; set; }
    
    //Change it, write it without Of
    public List<MedicalCard> MedicalCardsOfPatients { get; set; }
    
    public List<Appointment>? Appointmants { get; set; }
    
    public List<WorkingHours>? WorkingHours { get; set; }
}