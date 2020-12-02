using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using Moq;
using Shouldly;
using Xunit;

namespace HealthcareBaseTests
{
    public class DoctorAvailabilityTests
    {
        private readonly Mock<IShiftRepository> mockShiftRepository = new Mock<IShiftRepository>();
        private readonly Mock<IExaminationRepository> mockExaminationRepository = new Mock<IExaminationRepository>();
        private readonly DoctorAvailabilityService doctorAvailabilityService;

        public DoctorAvailabilityTests()
        {
            doctorAvailabilityService = new DoctorAvailabilityService(mockShiftRepository.Object,
                mockExaminationRepository.Object);
        }

        [Fact]
        public void Finds_one_doctor()
        {
            StubRepositories();
            doctorAvailabilityService.GetAvailableByDay(new DateTime(2020, 12, 10))
                .Count().ShouldBe(1);
        }

        [Fact]
        public void Finds_no_doctor()
        {
            StubRepositories();
            doctorAvailabilityService.GetAvailableByDay(new DateTime(2020, 12, 25))
                .Count().ShouldBe(0);
        }

        [Fact]
        public void Finds_both_doctors()
        {
            StubRepositories();
            doctorAvailabilityService.GetAvailableByDay(new DateTime(2020, 12, 15))
                .Count().ShouldBe(2);

        }
        private void StubRepositories()
        {
            var doctor1 = new Doctor
            {
                Id = 1,
                Person = new Person
                {
                    Name = "Marko",
                    Surname = "Markovic"
                }
            };
            var doctor2 = new Doctor
            {
                Id = 2,
                Person = new Person
                {
                    Name = "Mirko",
                    Surname = "Mirkovic"
                }
            };
            var date1 = new DateTime(2020, 12, 10);
            var date2 = new DateTime(2020, 12, 15);
            mockShiftRepository.Setup(s => s.GetByShiftStart(date1))
                .Returns(InitShifts(doctor1, doctor2, 1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor1, date1))
                .Returns(InitExaminations(doctor1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor2, date1))
                .Returns(InitExaminations(doctor2));

            mockShiftRepository.Setup(s => s.GetByShiftStart(date2))
                .Returns(InitShifts(doctor1, doctor2, 2));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor1, date2))
                .Returns(InitExaminations(doctor1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor2, date2))
                .Returns(InitExaminations(doctor2));

        }

        private IEnumerable<Examination> InitExaminations(Doctor doctor)
        {
            if (doctor.Id == 1)
                return new List<Examination>
                {
                    new Examination
                    {
                        Doctor = doctor,
                        DoctorId = doctor.Id,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 0, 0),
                            End = new DateTime(2020, 12, 10, 8, 30, 0)
                        }
                    },
                    new Examination
                    {
                        Doctor = doctor,
                        DoctorId = doctor.Id,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 30, 0),
                            End = new DateTime(2020, 12, 10, 9, 0, 0)
                        }
                    }
                };
            return new List<Examination>
            {
                new Examination
                {
                    Doctor = doctor,
                    DoctorId = doctor.Id,
                    TimeInterval = new TimeInterval
                    {
                        Start = new DateTime(2020, 12, 10, 8, 0, 0),
                        End = new DateTime(2020, 12, 10, 8, 30, 0)
                    }
                }
            };

        }

        private static IEnumerable<Shift> InitShifts(Doctor doctor1, Doctor doctor2, int flag)
        {
            return flag switch
            {
                1 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor1,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 0, 0),
                            End = new DateTime(2020, 12, 10, 9, 0, 0)
                        }
                    },
                    new Shift
                    {
                        Doctor = doctor2,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 0, 0),
                            End = new DateTime(2020, 12, 10, 10, 0, 0)
                        }
                    }
                },
                2 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor1,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 15, 8, 0, 0),
                            End = new DateTime(2020, 12, 15, 9, 0, 0)
                        }
                    },
                    new Shift
                    {
                        Doctor = doctor2,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 15, 8, 0, 0),
                            End = new DateTime(2020, 12, 15, 10, 0, 0)
                        }
                    }
                },
                _ => null
            };
        }
    }
}