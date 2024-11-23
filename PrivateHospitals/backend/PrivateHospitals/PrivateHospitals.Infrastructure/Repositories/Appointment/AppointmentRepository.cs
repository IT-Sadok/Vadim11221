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

    public async Task<FilteredResult<Core.Models.Appointment>> GetAppointmentsByFilter(AppointmentFilter filter, Paging paging, string patientId)
    {
        var query = _context.Set<Core.Models.Appointment>()
            .AsQueryable();

        if (filter.Speciality.HasValue)
        {
            query = query
                .Where(x => x.Doctor.DoctorSpeciality == filter.Speciality && x.PatientId == patientId);
        }
        
        if (filter.FromDate.HasValue)
        {
            query = query
                .Where(x => x.PatientId == patientId && x.AppointmentDate >= filter.FromDate);
        }
        
        if (filter.ToDate.HasValue)
        {
            query = query
                .Where(x => x.PatientId == patientId && x.AppointmentDate <= filter.ToDate);
        }

        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((paging.Page - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .Select(x => new Core.Models.Appointment
            {
                Doctor = new Core.Models.Users.Doctor()
                {
                    FirstName = x.Doctor.FirstName,
                    LastName = x.Doctor.LastName,
                    DoctorSpeciality = x.Doctor.DoctorSpeciality
                },
                Patient = new Core.Models.Users.Patient()
                {
                    FirstName = x.Patient.FirstName,
                    LastName = x.Patient.LastName,
                },
                AppointmentDate = x.AppointmentDate
            })
            .ToListAsync();

        return new FilteredResult<Core.Models.Appointment>()
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
