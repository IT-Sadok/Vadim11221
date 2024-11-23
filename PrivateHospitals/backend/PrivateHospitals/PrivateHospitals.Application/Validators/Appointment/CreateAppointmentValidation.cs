using FluentValidation;
using PrivateHospitals.Application.Dtos.Appointment;

namespace PrivateHospitals.Application.Validators.Appointment;

public class CreateAppointmentValidation: AbstractValidator<CreateAppointmentDto>
{
    public CreateAppointmentValidation()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorID is required");
            
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required");
    }
}