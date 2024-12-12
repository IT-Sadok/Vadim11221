using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Core.Models;

public class WorkingHours
{
    [Key]
    public string WorkingHoursId { get; set; }
    public DayOfWeek Day {get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}

