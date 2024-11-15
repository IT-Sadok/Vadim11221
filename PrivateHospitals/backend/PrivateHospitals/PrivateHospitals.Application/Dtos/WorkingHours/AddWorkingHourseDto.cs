namespace PrivateHospitals.Application.Dtos.WorkingHours;

public record AddWorkingHourseDto
{
    public required string DoctorId { get; init; }
    public required DateOnly Date { get; init; }
    public required TimeSpan StartTime { get; init; }
    public required TimeSpan EndTime { get; init; }
}