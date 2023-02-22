﻿using System.Diagnostics.CodeAnalysis;

namespace Appointments_API.Models.Dto;

[ExcludeFromCodeCoverage]
public class AppointmentDto
{
    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid ServiceId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public bool IsApproved { get; set; }
}
