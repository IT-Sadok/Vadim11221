using FluentValidation;
using PrivateHospitals.Application.Dtos.Appointment;

namespace PrivateHospitals.Application.Validators.Appointment;

public class CreateAppointmentValidation: AbstractValidator<CreateAppointmentDto>
{
    public CreateAppointmentValidation()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientID is required");

        RuleFor(x => x.DoctorFirstName)
            .NotEmpty().WithMessage("Doctor first name is required")
            .Matches("^[A-Za-z]+$").WithMessage("Doctor first name must contain only letters");
        
        RuleFor(x => x.DoctorLastName)
            .NotEmpty().WithMessage("Doctor last name is required")
            .Matches("^[A-Za-z]+$").WithMessage("Doctor last name must contain only letters");
        
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required");
        
        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Time is required");
    }
}