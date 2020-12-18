using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HospitalWebApp.Dtos;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Xunit;

namespace WPFHospitalEditorUnitTests
{
    public class SearchAppointmentTest
    {
        private readonly Mock<IShiftRepository> mockShiftRepository = new Mock<IShiftRepository>();
        private readonly Mock<IExaminationRepository> mockExaminationRepository = new Mock<IExaminationRepository>();
        private readonly Mock<IDoctorRepository> mockDoctorRepository = new Mock<IDoctorRepository>();
        private readonly ExaminationService examinationService;
        private Doctor doctor1;
        private Doctor doctor2;
        private Doctor doctor3;
        private TimeInterval timeInterval1;
        private TimeInterval timeInterval2;
        private TimeInterval timeInterval3;
        private Room room1;
        private Room room2;
        private Shift shift1;
        private Shift shift2;
        private Shift shift5;
        private RecommendationRequestDto dto1;
        private RecommendationRequestDto dto2;
        private RecommendationRequestDto dto3;
        private RecommendationRequestDto dto4;
        private RecommendationRequestDto dto5;
        private RecommendationRequestDto dto6;
        private List<Shift> shifts1 = new List<Shift>();
        private List<Shift> shifts2 = new List<Shift>();
        private List<Shift> shifts3 = new List<Shift>();

        public SearchAppointmentTest()
        {
            
            examinationService = new ExaminationService(mockExaminationRepository.Object, mockShiftRepository.Object, mockDoctorRepository.Object);


            InitDoctors();
            InitTimeInterval();
            InitRooms();
            InitDoctorShifts();
            InitRecommendationDto();
        }

        [Fact]
        public void Find_5_appointments_time_priority()
        {

            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();

            examinationService.Recommend(dto1).Count.ShouldBe(5);
        }

        [Fact]
        public void Find_3_appointments_time_priority()
        {
            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();
            
            examinationService.Recommend(dto2).Count.ShouldBe(3);
        }

        [Fact]
        public void Find_0_appointments_time_priority()
        {
            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();

            examinationService.Recommend(dto3).Count.ShouldBe(0);
        }

        [Fact]
        public void Find_5_appointments_with_doctor_priority()
        {
            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();

            examinationService.Recommend(dto4).Count.ShouldBe(5);
        }

        [Fact]
        public void Find_3_appointments_with_doctor_priority()
        {
            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();

            examinationService.Recommend(dto5).Count.ShouldBe(3);
        }

        [Fact]
        public void Find_0_appointments_with_doctor_priority()
        {
            StubRepositories();
            List<RecommendationDto> availableAppointments = new List<RecommendationDto>();

            examinationService.Recommend(dto6).Count.ShouldBe(0);
        }

        private void StubRepositories()
        {
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor1.Id, timeInterval1)).Returns(shifts1);
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor2.Id, timeInterval2)).Returns(shifts2);
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor3.Id, timeInterval3)).Returns(shifts3);
        }

        private void InitDoctorShifts()
        {

            shift1 = new Shift
            {
                Doctor = doctor1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 11, 8, 0, 0),
                    End = new DateTime(2020, 12, 11, 16, 0, 0)
                },
                AssignedExamRoomId = room1.Id
            };

            shift2 = new Shift
            {
                Doctor = doctor2,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 12, 8, 0, 0),
                    End = new DateTime(2020, 12, 12, 9, 30, 0)
                },
                AssignedExamRoomId = room2.Id
            };           
            shift5 = new Shift
            {
                Doctor = doctor1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 15, 8, 0, 0),
                    End = new DateTime(2020, 12, 15, 16, 0, 0)
                },
                AssignedExamRoomId = room1.Id
            };           
            

            shifts1.Add(shift1);
            shifts1.Add(shift5);
            shifts2.Add(shift2);           
        }      


        private void InitDoctors()
        {
            doctor1 = new Doctor
            {
                Id = 1,
                Person = new Person
                {
                    Name = "Marko",
                    Surname = "Markovic"
                },
                Department = new Department
                {
                    Id = 1,
                    Name = "Opsta praksa"
                }
            };
            doctor2 = new Doctor
            {
                Id = 2,
                Person = new Person
                {
                    Name = "Mirko",
                    Surname = "Mirkovic"
                },
                Department = new Department
                {
                    Id = 1,
                    Name = "Opsta praksa"
                }
            };
            doctor3 = new Doctor
            {
                Id = 3,
                Person = new Person
                {
                    Name = "Petar",
                    Surname = "Petrovic"
                },
                Department = new Department
                {
                    Id = 1,
                    Name = "Opsta praksa"
                }
            };           
        }
        private void InitTimeInterval()
        {
            DateTime startDate1 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate1 = new DateTime(2020, 12, 10, 16, 0, 0);
            timeInterval1 = new TimeInterval(startDate1, endDate1);

            DateTime startDate2 = new DateTime(2020, 12, 11, 8, 0, 0);
            DateTime endDate2 = new DateTime(2020, 12, 11, 16, 0, 0);
            timeInterval2 = new TimeInterval(startDate1, endDate1);

            DateTime startDate3 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate3 = new DateTime(2020, 12, 10, 10, 0, 0);
            timeInterval3 = new TimeInterval(startDate1, endDate1);
        }

        private void InitRooms()
        {
            room1 = new Room
            {
                Id = 1,
                Name = "Opsti pregled 1"
            };
            room2 = new Room
            {
                Id = 2,
                Name = "Opsti pregled 2"
            };           
        }

        private void InitRecommendationDto()
        {
            dto1 = new RecommendationRequestDto()
            {
                DoctorId = doctor1.Id,
                TimeInterval = timeInterval1,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            };

            dto2 = new RecommendationRequestDto()
            {
                DoctorId = doctor2.Id,
                TimeInterval = timeInterval2,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            };

            dto3 = new RecommendationRequestDto()
            {
                DoctorId = doctor3.Id,
                TimeInterval = timeInterval3,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            };

            dto4 = new RecommendationRequestDto()
            {
                DoctorId = doctor1.Id,
                TimeInterval = timeInterval1,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            };
            dto5 = new RecommendationRequestDto()
            {
                DoctorId = doctor2.Id,
                TimeInterval = timeInterval2,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            };

            dto6 = new RecommendationRequestDto()
            {
                DoctorId = doctor3.Id,
                TimeInterval = timeInterval3,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            };
        }
    }
}
