using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Core.Models;

public class WorkingHours
{   
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}

