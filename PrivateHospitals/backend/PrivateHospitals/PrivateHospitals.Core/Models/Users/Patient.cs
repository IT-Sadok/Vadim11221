using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateHospitals.Core.Models.Users;

public class Patient: AppUser
{
     public Patient()
     {
          Role = Roles.Patient;
     }
     
     [ForeignKey("MedicalCard")]
     public int? MedicalCardId { get; set; }
     public MedicalCard? MedicalCard { get; set; }

     public List<Appointment>? Appointmants { get; set; }
}