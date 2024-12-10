using FluentValidation;
using PrivateHospitals.Application.Dtos.DoctorInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateHospitals.Application.Validators.DoctorInfo
{
    public class DoctorInfoValidation: AbstractValidator<DoctorInfoDto>
    {
        public DoctorInfoValidation()
        {
            RuleFor(x => x.DoctorInfoId)
                .NotEmpty().WithMessage("DoctorInfoId is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.University)
                .NotEmpty().WithMessage("University is required");

            RuleFor(x => x.DiplomNumber)
                .NotEmpty().WithMessage("DiplomeNumber is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");
        }
    }
}
