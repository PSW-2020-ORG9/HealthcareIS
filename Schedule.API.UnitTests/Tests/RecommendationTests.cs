using System;
using System.Collections.Generic;
using General;
using Moq;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Recommendations;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;
using Schedule.API.Services.Procedures;
using Xunit;

namespace Schedule.API.UnitTests.Tests
{
    public class RecommendationTests
    {
        private readonly Mock<IShiftRepository> shiftRepository = new Mock<IShiftRepository>();
        private readonly Mock<IExaminationRepository> examinationRepository = new Mock<IExaminationRepository>();
        private readonly Mock<IConnection> connection = new Mock<IConnection>();

        
        private TimeInterval okInterval = new TimeInterval
        {
            Start = new DateTime(2022, 1, 1, 0, 0, 0),
            End = new DateTime(2022, 1, 3, 0, 0, 0)
        };
        private TimeInterval emptyInterval = new TimeInterval
        {
            Start = new DateTime(2023, 1, 1, 0, 0, 0),
            End = new DateTime(2023, 1, 3, 0, 0, 0)
        };
        
        private void PrepareStubs()
        {
            List<Shift> doctorOneShifts = new List<Shift>();
            List<Shift> otherDoctorShifts = new List<Shift>();
            
            doctorOneShifts.Add(new Shift
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
            doctorOneShifts.Add(new Shift
            {
                Id = 1,
                DoctorId = 1,
                AssignedExamRoomId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 1, 2, 8, 0, 0),
                    End = new DateTime(2022, 1, 2, 16, 0, 0)
                }
            });

            shiftRepository.Setup(m => m.GetByDoctorIdAndTimeInterval(1, okInterval)).Returns(doctorOneShifts);
            examinationRepository.Setup(m => m.GetByDoctorAndExaminationStart(1, okInterval.Start)).Returns(new List<Examination>());
            
        }

        [Fact]
        public void Gets_doctor_priority_recommendation()
        {
            PrepareStubs();
            RecommendationService recommendationService = new RecommendationService(
                examinationRepository.Object, 
                shiftRepository.Object,
                connection.Object);
            RecommendationRequestDto dto = new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Doctor,
                SpecialtyId = 1,
                TimeInterval = null
            };

            IEnumerable<RecommendationDto> result = recommendationService.Recommend(dto);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Gets_time_priority_recommendation()
        {
            PrepareStubs();
            RecommendationService recommendationService = new RecommendationService(
                examinationRepository.Object,
                shiftRepository.Object,
                connection.Object
            );
            RecommendationRequestDto dto = new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Time,
                SpecialtyId = 1,
                TimeInterval = null
            };

            IEnumerable<RecommendationDto> result = recommendationService.Recommend(dto);
            Assert.NotEmpty(result);
        }
    }
}
