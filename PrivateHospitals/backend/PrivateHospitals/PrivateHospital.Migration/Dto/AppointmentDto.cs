using PrivateHospitals.Core.Enum;

namespace PrivateHospital.Migration.Dto;

public record AppointmentDto
{
    public string ExternalId { get; init; }
    public string DoctorExternalId { get; init; }
    public string PatientExternalId { get; init; }
    public AppointmentStatuses Status { get; init; } = AppointmentStatuses.Scheduled;
    public DateTime Date { get; init; }

}