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
        private readonly DoctorAvailabilityService doctorAvailabilityService;
        private readonly ExaminationService examinationService;
        private Doctor doctor1;
        private Doctor doctor2;
        private Doctor doctor3;
        private Doctor doctor4;
        private TimeInterval timeInterval1;
        private TimeInterval timeInterval2;
        private TimeInterval timeInterval3;
        private TimeInterval timeInterval4;
        private Room room1;
        private Room room2;
        private Room room3;
        private Shift shift1;
        private Shift shift2;
        private Shift shift3;
        private Shift shift4;
        private Shift shift5;
        private Shift shift6;
        private Shift shift7;
        private Shift shift8;
        private RecommendationRequestDto dto1;
        private RecommendationRequestDto dto2;
        private RecommendationRequestDto dto3;
        private RecommendationRequestDto dto4;
        private RecommendationRequestDto dto5;
        private RecommendationRequestDto dto6;
        private Examination examination1;
        private Examination examination2;
        private Examination examination3;
        private Examination examination4;
        private List<Examination> examinations1 = new List<Examination>();
        private List<Examination> examinations2 = new List<Examination>();
        private List<Examination> examinations3 = new List<Examination>();
        private List<Shift> shifts1 = new List<Shift>();
        private List<Shift> shifts2 = new List<Shift>();
        private List<Shift> shifts3 = new List<Shift>();
        private List<Shift> shifts4 = new List<Shift>();

        public SearchAppointmentTest()
        {
            doctorAvailabilityService = new DoctorAvailabilityService(mockShiftRepository.Object,
                mockExaminationRepository.Object);
            examinationService = new ExaminationService(mockExaminationRepository.Object, mockShiftRepository.Object, mockDoctorRepository.Object);


            InitDoctors();
            InitTimeInterval();
            InitRooms();
            InitDoctorShifts();
            InitExaminations();
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
            shift3 = new Shift
            {
                Doctor = doctor3,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 13, 8, 0, 0),
                    End = new DateTime(2020, 12, 13, 16, 0, 0)
                },
                AssignedExamRoomId = room3.Id
            };
            shift4 = new Shift
            {
                Doctor = doctor4,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 14, 8, 0, 0),
                    End = new DateTime(2020, 12, 14, 10, 0, 0)
                },
                AssignedExamRoomId = room3.Id
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

            shift6 = new Shift
            {
                Doctor = doctor2,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 16, 8, 0, 0),
                    End = new DateTime(2020, 12, 16, 16, 0, 0)
                },
                AssignedExamRoomId = room2.Id
            };
            shift7 = new Shift
            {
                Doctor = doctor3,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 17, 8, 0, 0),
                    End = new DateTime(2020, 12, 17, 16, 0, 0)
                },
                AssignedExamRoomId = room3.Id
            };
            shift8 = new Shift
            {
                Doctor = doctor3,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 18, 8, 0, 0),
                    End = new DateTime(2020, 12, 18, 16, 0, 0)
                },
                AssignedExamRoomId = room3.Id
            };

            shifts1.Add(shift1);
            shifts1.Add(shift5);
            shifts2.Add(shift2);
            //shifts2.Add(shift6);
            //shifts3.Add(shift3);
            //shifts3.Add(shift7);
            shifts4.Add(shift4);
            shifts4.Add(shift8);
        }

        private void InitExaminations()
        {
            examination1 = new Examination
            {
                Doctor = doctor1,
                DoctorId = doctor1.Id,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 8, 0, 0),
                    End = new DateTime(2020, 12, 10, 8, 30, 0)
                },
                Room = room1,
                RoomId = room1.Id
            };
            examination2 = new Examination
            {
                Doctor = doctor2,
                DoctorId = doctor2.Id,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 12, 8, 0, 0),
                    End = new DateTime(2020, 12, 12, 8, 30, 0)
                },
                Room = room2,
                RoomId = room2.Id
            };

            examination3 = new Examination
            {
                Doctor = doctor3,
                DoctorId = doctor3.Id,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 8, 0, 0),
                    End = new DateTime(2020, 12, 10, 8, 30, 0)
                },
                Room = room3,
                RoomId = room3.Id
            };
            examination4 = new Examination
            {
                Doctor = doctor4,
                DoctorId = doctor4.Id,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 9, 30, 0),
                    End = new DateTime(2020, 12, 10, 10, 00, 0)
                },
                Room = room1,
                RoomId = room1.Id
            };
            examinations2.Add(examination2);
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
            doctor4 = new Doctor
            {
                Id = 4,
                Person = new Person
                {
                    Name = "Pavle",
                    Surname = "Pavlovic"
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

            DateTime startDate4 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate4 = new DateTime(2020, 12, 10, 16, 0, 0);
            timeInterval4 = new TimeInterval(startDate1, endDate1);
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
            room3 = new Room
            {
                Id = 2,
                Name = "Opsti pregled 3"
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
