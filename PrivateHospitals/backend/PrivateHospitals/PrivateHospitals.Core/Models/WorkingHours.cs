using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Core.Models;

public class WorkingHours
{
    [Key] public int HoursId { get; set; }
    
    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}