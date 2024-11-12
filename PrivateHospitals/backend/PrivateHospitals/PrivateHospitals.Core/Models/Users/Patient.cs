using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateHospitals.Core.Models.Users;

public class Patient: AppUser
{
     [ForeignKey("MedicalCard")]
     public int? MedicalCardId { get; set; }
     public MedicalCard? MedicalCard { get; set; }
}