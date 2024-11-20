using PrivateHospitals.Core.Enum;

namespace PrivateHospitals.Core.Models;

public class AppointmentFilter
{
    public DoctorSpecialities? Speciality { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}