using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Application.Dtos.Appointment;

public record AppointmentFilterDto
{
    public DoctorSpecialities? Speciality { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}