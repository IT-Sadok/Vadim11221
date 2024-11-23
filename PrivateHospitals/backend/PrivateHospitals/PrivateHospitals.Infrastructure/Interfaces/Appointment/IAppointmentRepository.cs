using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Infrastructure.Interfaces.Appointment;

public interface IAppointmentRepository
{
    Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment);
    Task<FilteredResult<Core.Models.Appointment>> GetAppointmentsByFilter(AppointmentFilter filter, Paging pagin, string patientId);
}