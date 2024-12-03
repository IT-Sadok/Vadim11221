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

        public UnitOfWork(HospitalDbContext context, IRepository<Doctor> _doctorRepository, IExternalIdRepository<Doctor> _doctorExternalIdRepository, IRepository<Patient> _patientRepository, IExternalIdRepository<Patient> _patientExternalIdRepository , IRepository<Appointment> _appointmentRepository, IExternalIdRepository<Appointment> _appointmentExternalIdRepository)
        {
            _context = context;
            DoctorService = new DoctorService(_doctorRepository, _doctorExternalIdRepository);
            PatientService = new PatientService(_patientRepository, _patientExternalIdRepository);
            AppointmentService = new AppointmentService(_appointmentRepository, _appointmentExternalIdRepository);
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
