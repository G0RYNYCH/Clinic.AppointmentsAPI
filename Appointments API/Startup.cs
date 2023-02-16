﻿using Appointments_API.Interfaces;
using Appointments_API.Repositories;
using Appointments_API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Appointments_API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddMvc();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<IValidator>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            {
                options.MapType<DateOnly>(() =>
                new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new OpenApiString("2023-01-01")
                });
                options.MapType<TimeOnly>(() =>
                new OpenApiSchema
                {
                    Type = "string",
                    Format = "time",
                    Example = new OpenApiString("12:01:01")
                });
            });
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<AppointmentDbContext>(x =>
                x.UseNpgsql(Configuration.GetConnectionString("DbConnection")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var logger = new LoggerConfiguration()
            .CreateLogger();
        loggerFactory.AddSerilog(logger, true);

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //app.UseSerilog((context, configuration) =>
        //{
        //    configuration.Enrich.FromLogContext()
        //        .Enrich.WithMachineName()
        //        .WriteTo.Console()
        //        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions());
        //});
    }
}