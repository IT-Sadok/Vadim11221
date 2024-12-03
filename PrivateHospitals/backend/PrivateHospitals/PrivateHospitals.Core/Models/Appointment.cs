using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Core.Models;

public class Appointment
{
    [Key] 
    public int AppointmentId { get; set; }
    public string ExternalId { get; set; }
    public string CompanyId { get; set; }
    public DateTime AppointmentDate { get; set; }

    public AppointmentStatuses Status { get; set; } = AppointmentStatuses.Scheduled;

    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    [ForeignKey("Patient")]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }
}