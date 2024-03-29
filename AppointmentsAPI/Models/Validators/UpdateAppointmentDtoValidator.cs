﻿using AppointmentsAPI.Models.Dto;
using FluentValidation;

namespace AppointmentsAPI.Models.Validators;

public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
{
    public UpdateAppointmentDtoValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty()
            .Must(x => x != Guid.Empty);

        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .Must(x => x != Guid.Empty);

        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .Must(x => x != Guid.Empty);

        RuleFor(x => x.Date)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Appointment date must be greater than current date");

        RuleFor(x => x.Time)
            .NotEmpty();
    }
}
