﻿using Appointments_API.Models;
using Appointments_API.Models.Dto;
using AutoMapper;

namespace Appointments_API.Models.Mapping;

public class UpdateAppointmentProfile : Profile
{
    public UpdateAppointmentProfile()
    {
        CreateMap<UpdateAppointmentDto, Appointment>();
        CreateMap<Appointment, UpdateAppointmentDto>();
    }
}