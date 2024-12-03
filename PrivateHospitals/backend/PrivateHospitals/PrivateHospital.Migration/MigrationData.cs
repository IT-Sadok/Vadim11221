
using PrivateHospital.Migration.Dto;

public class MigrationData
{
    public List<DoctorDto> Doctors { get; set; }
    public List<PatientDto> Patients { get; set; }
    public List<AppointmentDto> Appointments { get; set; }
}