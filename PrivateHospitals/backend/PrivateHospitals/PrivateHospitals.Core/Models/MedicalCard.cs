using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateHospitals.Core.Models;

public class MedicalCard
{
    public int MedicalCardId { get; set; }
    public DateTime VisitDate { get; set; }

    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    
    [ForeignKey("Patient")]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }
}