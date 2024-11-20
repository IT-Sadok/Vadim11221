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
    
     public async Task<bool> UpdateWorkingHoursAsync(string doctorId, List<WorkingHours> workingHours)
     {
        var doctor = await GetDoctorByIdAsync(doctorId);
        doctor.WorkingHours = workingHours;
        
        _context.Users.Update(doctor);
        await _context.SaveChangesAsync();

        return true;
     }

     public async Task<bool> IsDoctorAvailableAsync(string doctorId, DateTime appointmentDate)
     {
         var doctor = await GetDoctorByIdAsync(doctorId);
         
         var appointmentDay = appointmentDate.DayOfWeek;
         var appointmentTime = appointmentDate.TimeOfDay;

         foreach (var item in doctor.WorkingHours)
         {
             if (item.Day == appointmentDay)
             {
                 if (item.StartTime <= appointmentTime && item.EndTime >= appointmentTime)
                 {
                     return true;
                 }
             }
         }

         return false;
     }

     public async Task<List<Core.Models.Users.Doctor>> GetWorkingHoursAsync(string doctorId)
     {
         var doctor = await GetDoctorByIdAsync(doctorId);

         var workingHours = await _context.Users
             .OfType<Core.Models.Users.Doctor>()
             .Where(x => x.Id == doctorId)
             .Select(x => new Core.Models.Users.Doctor
             {
                 FirstName = x.FirstName,
                 LastName = x.LastName,
                 WorkingHours = x.WorkingHours
             }).ToListAsync();
         
         return workingHours;
     }
     
}