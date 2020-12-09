using HealthcareBase.Model.Database;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.MedicationRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Repository.UsersRepository.GeneralitiesRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.MiscellaneousService;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using HealthcareBase.Service.UsersService.UserFeedbackService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace HospitalWebAppIntegrationTests
{
    public class HospitalWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
        }
    }
}