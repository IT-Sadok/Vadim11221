using PrivateHospital.Migration.Dto;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospitals.Core.Enum;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospital.Migration.Services
{
    public class AppointmentService(
            IRepository<Appointment> _appointmentRepository,
            IRepository<Doctor> _doctorRepository,
            IRepository<Patient> _patientRepository
        )
    {
        public async Task SaveAppointmentAsync(AppointmentDto appointmentDto)
        {
            if(appointmentDto != null)
            {
                var doctor = await _doctorRepository.GetByExternalId(appointmentDto.DoctorExternalId);
                var patient = await _patientRepository.GetByExternalId(appointmentDto.PatientExternalId);

                if(doctor != null && patient != null)
                {
                    var appointment = new Appointment
                    {
                        ExternalId = appointmentDto.ExternalId,
                        DoctorId = doctor.Id,
                        PatientId = patient.Id,
                        AppointmentDate = appointmentDto.Date.ToUniversalTime(),
                        Status = AppointmentStatuses.Scheduled
                    };

                    await _appointmentRepository.AddAsync(appointment);
                }
            }
        }
    }
}
