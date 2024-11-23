namespace PrivateHospitals.Application.Dtos.Doctor;

public record DoctorUpdateHours()
{
    public required string DoctorId { get; init; }
    public required List<Core.Models.WorkingHours> WorkingHours { get; init; }
};