﻿using System;
using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using Moq;
using Xunit;

namespace HealthcareBaseTests
{
    public class ExaminationTests
    {
        private Mock<IExaminationRepository> _examinationRepository;
        private Mock<IShiftRepository> _shiftRepository;
        private Mock<IDoctorRepository> _doctorRepository;

        private void PrepareStubs()
        {
            _examinationRepository = new Mock<IExaminationRepository>();
            _shiftRepository = new Mock<IShiftRepository>();
            _doctorRepository = new Mock<IDoctorRepository>();
            
            var examinations = new List<Examination>();
            examinations.Add(new Examination());

            _examinationRepository.Setup(e => e.GetByPatientId(1))
                .Returns(examinations);
            _examinationRepository.Setup(e => e.GetByPatientId(5))
                .Returns(new List<Examination>());
            _examinationRepository.Setup(e => e.GetByID(1))
                .Returns(new Examination()
                {
                    IsCanceled = false,
                    TimeInterval = new TimeInterval(DateTime.Now.AddDays(3), DateTime.Now)
                });
            _examinationRepository.Setup(e => e.GetByID(5))
                .Returns(new Examination() {IsCanceled = true});
            _examinationRepository.Setup(e => e.GetByID(10))
                .Returns<IEnumerable<Examination>>(default);
            _examinationRepository.Setup(e => e.GetByID(12))
                .Returns(new Examination()
                {
                    TimeInterval = new TimeInterval(DateTime.Now, DateTime.Now)
                });
            _examinationRepository.Setup(e => e.GetByID(12))
                .Returns(new Examination()
                {
                    TimeInterval = new TimeInterval(DateTime.Now, DateTime.Now)
                });

            _examinationRepository.Setup(e => e.Update(It.IsAny<Examination>()))
                .Returns(new Examination());
        }

        [Fact]
        public void Find_patient_examinations_success()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object, _doctorRepository.Object);

            var examinations = examinationService.GetByPatientId(1);
            Assert.NotEmpty(examinations);
        }
        
        [Fact]
        public void Find_patient_examinations_fail()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object, _doctorRepository.Object);

            var emptyExaminations = examinationService.GetByPatientId(5);
            Assert.Empty(emptyExaminations);
        }

        [Fact]
        public void Cancel_patient_examination_success()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object, _doctorRepository.Object);

            bool cancelResult = examinationService.Cancel(1);
            Assert.True(cancelResult);
        }

        [Fact]
        public void Cancel_patient_examination_fail_already_cancelled()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object, _doctorRepository.Object);

            bool cancelResult = examinationService.Cancel(5);
            Assert.False(cancelResult);
        }
        
        [Fact]
        public void Cancel_patient_examination_fail_not_found()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object, _doctorRepository.Object);

            bool cancelResult = examinationService.Cancel(10);
            Assert.False(cancelResult);
        }
        
        [Fact]
        public void Cancel_patient_examination_fail_too_late()
        {
            PrepareStubs();
            var examinationService = new ExaminationService(_examinationRepository.Object, _shiftRepository.Object);

            bool cancelResult = examinationService.Cancel(12);
            Assert.False(cancelResult);
        }
    }
}