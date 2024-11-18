using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Appointment;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Infrastructure.Repositories.Appointment;

public class AppointmentRepository(
    HospitalDbContext _context
): IAppointmentRepository
{
    public async Task<bool> CreateAppointmentAsync(Core.Models.Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Core.Models.Appointment>> GetAppointmentBySpecialityAsync(string patientId, DoctorSpecialities? speciality)
    {
        var appointments = await _context.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Where(x => x.Doctor.DoctorSpeciality == speciality && x.PatientId == patientId)
            .ToListAsync();

        return appointments;
    }

    public async Task<List<Core.Models.Appointment>> GetAppointmentsByDateAsync(string patientId, DateTime? fromDate, DateTime? toDate)
    {
        var appointments = await _context.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Where(x => x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate && x.PatientId == patientId)
            .ToListAsync();

        return appointments;
    }
}