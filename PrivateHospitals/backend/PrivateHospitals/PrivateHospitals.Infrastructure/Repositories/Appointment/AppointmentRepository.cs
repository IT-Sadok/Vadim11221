using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
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

    public async Task<PaginationResult<Core.Models.Appointment>> GetAppointmentBySpecialityAsync(
        AppointmentFilter appointmentFilter, string patientId)
    {
        var totalItems = await _context.Appointments
            .AsNoTracking()
            .Where(x => x.Doctor.DoctorSpeciality == appointmentFilter.Speciality && x.PatientId == patientId)
            .CountAsync();

        var items = await _context.Appointments
            .AsNoTracking()
            .Where(x => x.Doctor.DoctorSpeciality == appointmentFilter.Speciality && x.PatientId == patientId)
            .OrderBy(x => x.AppointmentDate) // Сортування по AppointmentDate
            .Skip((appointmentFilter.PageNumber - 1) * appointmentFilter.PageSize)
            .Take(appointmentFilter.PageSize)
            .Select(x => new Core.Models.Appointment
            {
                AppointmentDate = x.AppointmentDate,
                Doctor = new Core.Models.Users.Doctor()
                {
                    FirstName = x.Doctor.FirstName,
                    LastName = x.Doctor.LastName
                },
                Patient = new Core.Models.Users.Patient()
                {
                    FirstName = x.Patient.FirstName,
                    LastName = x.Patient.LastName
                },
                Status = x.Status
            })
            .ToListAsync();

        var result = new PaginationResult<Core.Models.Appointment>(items, totalItems, appointmentFilter.PageNumber,
            appointmentFilter.PageSize);

        return result;
    }

    public async Task<PaginationResult<Core.Models.Appointment>> GetAppointmentsByDateAsync(AppointmentFilter appointmentFilter, string patientId)
    { 
        var totalItems = await _context.Appointments
            .AsNoTracking()
            .Where(x => x.AppointmentDate <= appointmentFilter.FromDate && x.AppointmentDate >= appointmentFilter.ToDate && x.PatientId == patientId)
            .CountAsync();
        
        var items = await _context.Appointments
            .AsNoTracking()
            .Where(x => x.AppointmentDate >= appointmentFilter.FromDate && x.AppointmentDate <= appointmentFilter.ToDate && x.PatientId == patientId)
            .OrderBy(x => x.AppointmentDate)
            .Skip((appointmentFilter.PageNumber - 1) * appointmentFilter.PageSize)
            .Take(appointmentFilter.PageSize)
            .Select(x => new Core.Models.Appointment
            {
                AppointmentDate = x.AppointmentDate,
                Doctor = new Core.Models.Users.Doctor()
                {
                    FirstName = x.Doctor.FirstName,
                    LastName = x.Doctor.LastName
                },
                Patient = new Core.Models.Users.Patient()
                {
                    FirstName = x.Patient.FirstName,
                    LastName = x.Patient.LastName
                },
                Status = x.Status
            }).ToListAsync();
        
        var result = new PaginationResult<Core.Models.Appointment>(items, totalItems, appointmentFilter.PageNumber,
            appointmentFilter.PageSize);

        return result;
    }
}