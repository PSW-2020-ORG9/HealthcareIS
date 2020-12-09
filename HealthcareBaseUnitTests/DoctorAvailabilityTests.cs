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
        private Doctor doctor1;
        private Doctor doctor2;
        private Doctor doctor3;
        private DateTime date1;
        private DateTime date2;

        private enum ShiftPreset
        {
            Preset1,
            Preset2
        }
        

        public DoctorAvailabilityTests()
        {
            doctorAvailabilityService = new DoctorAvailabilityService(mockShiftRepository.Object,
                mockExaminationRepository.Object);
            InitDoctors();
            InitDates();
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

        [Fact]
        public void Finds_no_term()
        {
            StubRepositories();
            doctorAvailabilityService.
                GetAvailableIntervals(1, new DateTime(2020, 12, 10))
                .ShouldBeEmpty();
        }

        [Fact]
        public void Finds_three_terms()
        {
            StubRepositories();

            var expectedIntervals = new List<TimeInterval>
            {
                new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 8, 30, 0),
                    End = new DateTime(2020, 12, 10, 9, 00, 0)
                },
                new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 9, 00, 0),
                    End = new DateTime(2020, 12, 10, 9, 30, 0)
                },
                new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 9, 30, 0),
                    End = new DateTime(2020, 12, 10, 10, 00, 0)
                }

            };
            doctorAvailabilityService.
                GetAvailableIntervals(2, new DateTime(2020, 12, 10))
                .ShouldBe(expectedIntervals);
            
        }

        [Fact]
        public void Finds_examinations_in_between()
        {
            StubRepositories();

            var expectedIntervals = new List<TimeInterval>
            {
                new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 8, 00, 0),
                    End = new DateTime(2020, 12, 10, 8, 30, 0)
                },
                new TimeInterval
                {
                    Start = new DateTime(2020, 12, 10, 9, 00, 0),
                    End = new DateTime(2020, 12, 10, 9, 30, 0)
                }

            };
            doctorAvailabilityService.
                GetAvailableIntervals(3, new DateTime(2020, 12, 10))
                .ShouldBe(expectedIntervals);
        }
        private void StubRepositories()
        {
            
            mockShiftRepository.Setup(s => s.GetByShiftStart(date1))
                .Returns(InitShifts(doctor1, doctor2, ShiftPreset.Preset1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor1.Id, date1))
                .Returns(InitExaminations(doctor1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor2.Id, date1))
                .Returns(InitExaminations(doctor2));

            mockShiftRepository.Setup(s => s.GetByShiftStart(date2))
                .Returns(InitShifts(doctor1, doctor2, ShiftPreset.Preset2));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor1.Id, date2))
                .Returns(InitExaminations(doctor1));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor2.Id, date2))
                .Returns(InitExaminations(doctor2));
            mockExaminationRepository.Setup(e => e.GetByDoctorAndDate(doctor3.Id, date1))
                .Returns(InitExaminations(doctor3));

            mockShiftRepository.Setup(s => s.GetByDoctorAndShiftStart(1, date1))
                .Returns(InitDoctorShifts(doctor1,date1));
            mockShiftRepository.Setup(s => s.GetByDoctorAndShiftStart(2, date1))
                .Returns(InitDoctorShifts(doctor2,date1));
            mockShiftRepository.Setup(s => s.GetByDoctorAndShiftStart(3, date1))
                .Returns(InitDoctorShifts(doctor3,date1));

        }

        private IEnumerable<Shift> InitDoctorShifts(Doctor doctor,DateTime date)
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
                            Start = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0),
                            End = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0)
                        }
                    }
                },
                2 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0),
                            End = new DateTime(date.Year, date.Month, date.Day, 10, 0, 0)
                        }
                    }
                },
                3 => new List<Shift>
                {
                    new Shift
                    {
                        Doctor = doctor,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0),
                            End = new DateTime(date.Year, date.Month, date.Day, 10, 0, 0)
                        }
                    }
                },
                _ => null
            };
        }

        private IEnumerable<Examination> InitExaminations(Doctor doctor)
        {
            return doctor.Id switch
            {
                1 => new List<Examination>
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
                },
                2 => new List<Examination>
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
                },
                _ => new List<Examination>
                {
                    new Examination
                    {
                        Doctor = doctor,
                        DoctorId = doctor.Id,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 8, 30, 0),
                            End = new DateTime(2020, 12, 10, 9, 00, 0)
                        }
                    },
                    new Examination
                    {
                        Doctor = doctor,
                        DoctorId = doctor.Id,
                        TimeInterval = new TimeInterval
                        {
                            Start = new DateTime(2020, 12, 10, 9, 30, 0),
                            End = new DateTime(2020, 12, 10, 10, 00, 0)
                        }
                    }
                }
            };
        }

        private static IEnumerable<Shift> InitShifts(Doctor doctor1, Doctor doctor2, ShiftPreset preset)
        {
            return preset switch
            {
                ShiftPreset.Preset1 => new List<Shift>
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
                ShiftPreset.Preset2 => new List<Shift>
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

        private void InitDoctors()
        {
            doctor1 = new Doctor
            {
                Id = 1,
                Person = new Person
                {
                    Name = "Marko",
                    Surname = "Markovic"
                }
            };
            doctor2 = new Doctor
            {
                Id = 2,
                Person = new Person
                {
                    Name = "Mirko",
                    Surname = "Mirkovic"
                }
            };
            doctor3 = new Doctor
            {
                Id = 3,
                Person = new Person
                {
                    Name = "Petar",
                    Surname = "Petrovic"
                }
            };
        }
        private void InitDates()
        {
            date1 = new DateTime(2020, 12, 10);
            date2 = new DateTime(2020, 12, 15);
        }
    }
}