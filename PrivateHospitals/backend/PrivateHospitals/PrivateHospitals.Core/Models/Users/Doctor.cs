using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models;

public class Doctor: AppUser
{ 
    public SpecialitiesOfDoctor Speciality { get; set; }
    
    public List<MedicalCard> MedicalCardsOfPatients { get; set; }
}