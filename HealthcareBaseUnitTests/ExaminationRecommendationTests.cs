using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HospitalWebApp.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HealthcareBaseUnitTests
{
    public class ExaminationRecommendationTests
    {
        ExaminationService examinationService;
        Mock<IShiftRepository> shiftRepository = new Mock<IShiftRepository>();
        Mock<IDoctorRepository> doctorRepository = new Mock<IDoctorRepository>();
        Mock<IExaminationRepository> examinationRepository = new Mock<IExaminationRepository>();
        TimeInterval timeInterval = new TimeInterval
        {
            Start = new DateTime(2022, 1, 1, 0, 0, 0),
            End = new DateTime(2022, 1, 3, 0, 0, 0)
        };

        private void PrepareStubs()
        {
            List<Shift> shifts = new List<Shift>();
            shifts.Add(new Shift
            {
                Id = 1,
                DoctorId = 1,
                AssignedExamRoomId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 1, 1, 8, 0, 0),
                    End = new DateTime(2022, 1, 1, 16, 0, 0)
                }
            });
            List<DoctorSpecialty> doctorSpecialties = new List<DoctorSpecialty>();
            doctorSpecialties.Add(new DoctorSpecialty
            {
                DoctorId = 1,
                SpecialtyId = 1
            });
            Doctor doctor = new Doctor
            {
                Id = 1,
                Specialties = doctorSpecialties,
            };
            shiftRepository.Setup(m => m.GetByDoctorIdAndTimeInterval(1, timeInterval)).Returns(shifts);
            doctorRepository.Setup(m => m.GetByID(1)).Returns(doctor);
            examinationRepository.Setup(m => m.GetByDoctorAndExaminationStart(1, timeInterval.Start)).Returns(new List<Examination>());
        }

        [Fact]
        public void Gets_doctor_priority_recommendation()
        {
            PrepareStubs();
            examinationService = new ExaminationService(examinationRepository.Object, shiftRepository.Object, doctorRepository.Object);
            RecommendationRequestDto dto = new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Doctor,
                SpecialtyId = 1,
                TimeInterval = timeInterval
            };

            RecommendationDto result = examinationService.Recommend(dto);
            Assert.NotNull(result);
        }

        [Fact]
        public void Gets_time_priority_recommendation()
        {
            PrepareStubs();
            examinationService = new ExaminationService(examinationRepository.Object, shiftRepository.Object, doctorRepository.Object);
            RecommendationRequestDto dto = new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Time,
                SpecialtyId = 1,
                TimeInterval = timeInterval
            };

            RecommendationDto result = examinationService.Recommend(dto);
            Assert.NotNull(result);
        }
    }
}
