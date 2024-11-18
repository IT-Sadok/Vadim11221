using System.Security.Cryptography.Xml;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Interfaces.Doctor;

namespace PrivateHospitals.Infrastructure.Repositories.Doctor;

public class DoctorRepository(
    HospitalDbContext _context
): IDoctorRepository
{
    public async Task<Core.Models.Users.Doctor> GetDoctorByIdAsync(string doctorId)
    {
        var doctor = await _context.Users
            .OfType<Core.Models.Users.Doctor>()
            .FirstOrDefaultAsync(x => x.Id == doctorId);
        
        return doctor;
    }

    public async Task<bool> UpdateWorkingHoursAsync(string doctorId, Schedule schedule)
    {
        var doctor = await GetDoctorByIdAsync(doctorId);    
        
        doctor.WorkingHoursJson = JsonSerializer.Serialize(schedule);
        
        _context.Users.Update(doctor);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsDoctorAvailableAsync(string doctorId, DateTime appointmentDate)
    {
        var doctor = await GetDoctorByIdAsync(doctorId);
        
        var schedule = JsonSerializer.Deserialize<Schedule>(doctor.WorkingHoursJson);
        
        var dayOfWeek = appointmentDate.DayOfWeek.ToString();

        if (!schedule.Days.ContainsKey(dayOfWeek))
        {
            return false;
        }
        
        var workingHours = schedule.Days[dayOfWeek];

        var appointmentTime = appointmentDate.TimeOfDay;

        if (appointmentTime >= workingHours.StartTime && appointmentTime <= workingHours.EndTime)
        {
            return true;
        }

        return false;
    }

    public async Task<Core.Models.Users.Doctor> GetDoctorBySpecialityAsync(DoctorSpecialities speciality)
    {
        var doctor = await _context.Users
            .OfType<Core.Models.Users.Doctor>()
            .FirstOrDefaultAsync(x => x.DoctorSpeciality == speciality);

        return doctor;
    }
}