using PrivateHospital.Migration.Dto;
using PrivateHospital.Migration.Dto.Interfaces;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospital.Migration.Interfaces;
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
            IExternalIdRepository<Appointment> _appointmentExternalId
        )
    {
        public async Task SaveAppointmentAsync(AppointmentDto appointmentDto)
        {
            var appointment = await _appointmentExternalId.GetByExternalId(appointmentDto.ExternalId);

            if(appointment == null)
            {
                     appointment = new Appointment
                    {
                        AppointmentId = appointmentDto.AppointmentId,
                        CompanyId = appointmentDto.CompanyId,
                        ExternalId = appointmentDto.ExternalId,
                        DoctorId = appointmentDto.Doctor.Id,
                        Doctor = appointmentDto.Doctor,
                        PatientId = appointmentDto.Patient.Id,
                        Patient = appointmentDto.Patient,
                        AppointmentDate = appointmentDto.Date.ToUniversalTime(),
                        Status = AppointmentStatuses.Scheduled
                    };

                    await _appointmentRepository.AddAsync(appointment);
            }
            else
            {
                Console.WriteLine("Appointment already exists: ExternalId = {ExternalId}", appointmentDto.ExternalId);
            }
        }
    }
}
