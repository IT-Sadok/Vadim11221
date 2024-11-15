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

    public async Task<List<Core.Models.Appointment>> GetAppointmentsByDate(DateOnly fromDate, DateOnly toDate, string patientId)
    {
        var appointments = await _context.Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .ToListAsync();
        
        return appointments;
    }

    public async Task<bool> IsDoctorHaveAppointment(DateOnly date, TimeSpan time, string doctorId)
    {
        var appointments = await _context.Appointments
            .Where(x => x.Date == date && x.DoctorId == doctorId)
            .ToListAsync();

        return appointments.Any(x => 
            (x.Time >= time && x.Time < time.Add(TimeSpan.FromMinutes(15))) ||
            (time >= x.Time && time < x.Time + TimeSpan.FromMinutes(15)));
    }
}