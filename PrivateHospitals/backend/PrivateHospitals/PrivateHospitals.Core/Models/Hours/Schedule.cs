namespace PrivateHospitals.Core.Models;

public class Schedule
{
    public Dictionary<string, WorkingHours> Days { get; set; } = new();
}