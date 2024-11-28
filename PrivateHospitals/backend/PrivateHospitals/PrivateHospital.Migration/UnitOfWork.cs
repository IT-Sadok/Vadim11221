using Microsoft.EntityFrameworkCore.Storage;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Services;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration
{
    public class UnitOfWork : IDisposable
    {
        private readonly HospitalDbContext _context;

        public UnitOfWork(HospitalDbContext context, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository, IRepository<Appointment> appointmentRepository)
        {
            _context = context;
            DoctorService = new DoctorService(doctorRepository);
            PatientService = new PatientService(patientRepository);
            AppointmentService = new AppointmentService(appointmentRepository ,doctorRepository, patientRepository);
        }

        public DoctorService DoctorService { get; }
        public PatientService PatientService { get; }
        public AppointmentService AppointmentService { get; }

        public async Task<int> SaveChangeAsync() => await _context.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync() => 
            await _context.Database.BeginTransactionAsync();

        public void Dispose() => _context.Dispose();
    }
}
