using FluentValidation;
using PrivateHospitals.Application.Dtos.Doctor;

namespace PrivateHospitals.Application.Validators.Doctor;

public class DoctorUpdateHoursValidation: AbstractValidator<DoctorUpdateHours>
{
    public DoctorUpdateHoursValidation()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required");

        RuleFor(x => x.WorkingHours)
            .NotEmpty().WithMessage("Working Hours is required");
    }
}