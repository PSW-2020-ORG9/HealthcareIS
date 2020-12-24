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
    public class RecommendAppointmentsTest
    {
        private readonly Mock<IShiftRepository> mockShiftRepository = new Mock<IShiftRepository>();
        private readonly Mock<IExaminationRepository> mockExaminationRepository = new Mock<IExaminationRepository>();
        private readonly Mock<IDoctorRepository> mockDoctorRepository = new Mock<IDoctorRepository>();
        private readonly ExaminationService examinationService;
        private static Dictionary<int, Doctor> doctors =  new Dictionary<int, Doctor>();
        private static List<TimeInterval> timeIntervals =  new List<TimeInterval>();
        private static Dictionary<int, Room> rooms = new Dictionary<int, Room>();

        public RecommendAppointmentsTest()
        {          
            examinationService = new ExaminationService(mockExaminationRepository.Object, mockShiftRepository.Object, mockDoctorRepository.Object);           
        }

        [Theory]
        [MemberData(nameof(Recommendation_request_data))]
        public void Find_appointments_time_priority(RecommendationRequestDto dto, int availableAppointments)
        {
            StubRepositories();

            examinationService.Recommend(dto).Count.ShouldBe(availableAppointments);
        }

        public static IEnumerable<object[]> Recommendation_request_data()
        {
            InitDoctors();
            InitRooms();

            var retVal = new List<object[]>();
            DateTime startDate1 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate1 = new DateTime(2020, 12, 10, 16, 0, 0);
            TimeInterval timeInterval1 = new TimeInterval(startDate1, endDate1);

            DateTime startDate2 = new DateTime(2020, 12, 11, 8, 0, 0);
            DateTime endDate2 = new DateTime(2020, 12, 11, 16, 0, 0);
            TimeInterval timeInterval2 = new TimeInterval(startDate2, endDate2);

            DateTime startDate3 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate3 = new DateTime(2020, 12, 10, 10, 0, 0);
            TimeInterval timeInterval3 = new TimeInterval(startDate3, endDate3);

            timeIntervals.Add(timeInterval1);
            timeIntervals.Add(timeInterval2);
            timeIntervals.Add(timeInterval3);

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[1].Id,
                TimeInterval = timeIntervals[0],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 5 });          

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[2].Id,
                TimeInterval = timeIntervals[1],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 3 });

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[3].Id,
                TimeInterval = timeIntervals[2],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 0 });

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[1].Id,
                TimeInterval = timeIntervals[0],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 5 });

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[2].Id,
                TimeInterval = timeIntervals[1],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 3 });

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = doctors[3].Id,
                TimeInterval = timeIntervals[2],
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 0 });

            return retVal;
        }
        
        private void StubRepositories()
        {
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctors[1].Id, timeIntervals[0])).Returns(InitShift(doctors[1]));
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctors[2].Id, timeIntervals[1])).Returns(InitShift(doctors[2]));
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctors[3].Id, timeIntervals[2])).Returns(InitShift(doctors[3]));
        }

        private IEnumerable<Shift> InitShift(Doctor doctor)
        {

            return doctor.Id switch
            {
                1 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 11, 8, 0, 0),
                            End = new DateTime(2020, 12, 11, 16, 0, 0)
                        },
                        AssignedExamRoomId = rooms[1].Id
                    }
                },
                2 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 0, 0),
                            End = new DateTime(2020, 12, 10, 9, 30, 0)
                        },
                        AssignedExamRoomId = rooms[2].Id
                    }
                },
                3 => new List<Shift>
                {
                    
                },
                _ => null
            };            
        }      

        private static void InitDoctors()
        {
            Doctor doctor1 = new Doctor
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
            AddDoctorToDictionary(doctor1);
            Doctor doctor2 = new Doctor
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
            AddDoctorToDictionary(doctor2);
            Doctor doctor3 = new Doctor
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
            AddDoctorToDictionary(doctor3);

        }

        private static void InitRooms()
        {
            Room room1 = new Room
            {
                Id = 1,
                Name = "Opsti pregled 1"
            };
            AddRoomToDictionary(room1);
            Room room2 = new Room
            {
                Id = 2,
                Name = "Opsti pregled 2"
            };
            AddRoomToDictionary(room2);
        }

        private static void AddDoctorToDictionary(Doctor doctor)
        {

            if (!doctors.ContainsKey(doctor.Id)) doctors.Add(doctor.Id, doctor);
        }

        private static void AddRoomToDictionary(Room room)
        {
            if (!rooms.ContainsKey(room.Id)) rooms.Add(room.Id, room);
        }
    }
}
