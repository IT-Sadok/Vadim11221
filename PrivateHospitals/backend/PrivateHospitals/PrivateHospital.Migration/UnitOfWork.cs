using Microsoft.EntityFrameworkCore.Storage;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospital.Migration.Interfaces;
using PrivateHospital.Migration.Services;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using PrivateHospitals.Infrastructure.Data;
using PrivateHospitals.Infrastructure.Repositories.Appointment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration
{
    public class UnitOfWork : IDisposable
    {
        private readonly HospitalDbContext _context;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(HospitalDbContext context, DoctorRepository _doctorRepository, PatientRepository _patientRepository, AppointmentsRepository _appointmentRepository)
        {
            _context = context;
            DoctorService = new DoctorService(_doctorRepository);
            PatientService = new PatientService(_patientRepository);
            AppointmentService = new AppointmentService(_doctorRepository, _patientRepository, _appointmentRepository);
        }

        public DoctorService DoctorService { get; }
        public PatientService PatientService { get; }
        public AppointmentService AppointmentService { get; }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitAsync()
        {
            if(_currentTransaction != null)
            {
                await _context.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollBackAsync()
        {
            if(_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            if(_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
