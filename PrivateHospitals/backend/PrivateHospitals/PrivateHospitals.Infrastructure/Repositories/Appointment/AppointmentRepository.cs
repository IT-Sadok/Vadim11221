using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Appointment;

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

    public async Task<List<Core.Models.Appointment>> GetAppointmentsBySpeciality(DoctorSpecialities speciality, string patientId)
    {
        var apointments = await _context.Appointments
            .Where(x => x.Doctor.DoctorSpeciality == speciality && x.PatientId == patientId)
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .ToListAsync();
        
        return apointments;
    }

    public async Task<List<Core.Models.Appointment>> GetAppointmentsByDate(DateTime fromDate, DateTime toDate, string patientId)
    {
        var appointments = await _context.Appointments
            .Where(x => x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate && x.PatientId == patientId)
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .ToListAsync();
        
        return appointments;
    }
}