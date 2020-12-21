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
        private static Doctor doctor1;
        private static Doctor doctor2;
        private static Doctor doctor3;
        private static TimeInterval timeInterval1;
        private static TimeInterval timeInterval2;
        private static TimeInterval timeInterval3;
        private static Room room1;
        private static Room room2;

        public SearchAppointmentTest()
        {
            
            examinationService = new ExaminationService(mockExaminationRepository.Object, mockShiftRepository.Object, mockDoctorRepository.Object);
            InitDoctors();
            InitRooms();
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
         
            var retVal = new List<object[]>();
            DateTime startDate1 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate1 = new DateTime(2020, 12, 10, 16, 0, 0);
            timeInterval1 = new TimeInterval(startDate1, endDate1);

            DateTime startDate2 = new DateTime(2020, 12, 11, 8, 0, 0);
            DateTime endDate2 = new DateTime(2020, 12, 11, 16, 0, 0);
            timeInterval2 = new TimeInterval(startDate2, endDate2);

            DateTime startDate3 = new DateTime(2020, 12, 10, 8, 0, 0);
            DateTime endDate3 = new DateTime(2020, 12, 10, 10, 0, 0);
            timeInterval3 = new TimeInterval(startDate3, endDate3);

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval1,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 5 });

            

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval2,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 3 });


            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval3,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Time
            }, 0 });

            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval1,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 5 });



            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval2,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 3 });


            retVal.Add(new object[] { new RecommendationRequestDto()
            {
                DoctorId = new Doctor
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
            }.Id,
                TimeInterval = timeInterval3,
                SpecialtyId = 1,
                Preference = RecommendationPreference.Doctor
            }, 0 });

            return retVal;
        }
        
        private void StubRepositories()
        {
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor1.Id, timeInterval1)).Returns(InitShift(doctor1));
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor2.Id, timeInterval2)).Returns(InitShift(doctor2));
            mockShiftRepository.Setup(s => s.GetByDoctorIdAndTimeInterval(doctor3.Id, timeInterval3)).Returns(InitShift(doctor3));
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
                        AssignedExamRoomId = room1.Id
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
                        AssignedExamRoomId = room2.Id
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

        private static void InitRooms()
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
    }
}
