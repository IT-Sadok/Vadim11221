using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models.Users;

public class Doctor: AppUser
{
    public DoctorSpecialities DoctorSpeciality { get; set; } = DoctorSpecialities.Doctor;
    
    public List<MedicalCard> MedicalCardsOfPatients { get; set; }
}