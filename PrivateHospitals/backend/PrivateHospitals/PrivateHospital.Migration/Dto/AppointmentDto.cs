using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospital.Migration.Dto;

public record AppointmentDto
{
    public string ExternalId { get; init; }
    public int AppointmentId { get; init; }
    public string CompanyId { get; init; }
    public Doctor Doctor { get; init; }
    public Patient Patient { get; init; }
    public AppointmentStatuses Status { get; init; } = AppointmentStatuses.Scheduled;
    public DateTime Date { get; init; }

}