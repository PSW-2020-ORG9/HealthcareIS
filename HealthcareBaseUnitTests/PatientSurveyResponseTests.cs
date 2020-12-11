using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HealthcareBaseUnitTests
{
    public class PatientSurveyResponseTests
    {
        private readonly Mock<ISurveyRepository> _mockSurveyRepository = new Mock<ISurveyRepository>();
        private readonly Mock<ISurveyResponseRepository> _mockSurveyResponseRepository = new Mock<ISurveyResponseRepository>();

        [Fact]
        public void Creates_survey_response()
        {
            SetupRepositories();
            SurveyResponseService service = new SurveyResponseService(_mockSurveyResponseRepository.Object);
            SurveyResponse response = new SurveyResponse();

            service.CreateSurveyResponse(response);

            _mockSurveyResponseRepository.Verify(m => m.Create(response), Times.Once);
        }

        [Fact]
        public void Gets_survey_for_patient()
        {
            SetupRepositories();
            SurveyService service = new SurveyService(_mockSurveyRepository.Object);

            Survey survey = service.GetById(1);

            Assert.NotNull(survey);
        }

        private void SetupRepositories()
        {
            _mockSurveyRepository.Setup(m => m.GetByID(It.IsAny<int>())).Returns(CreateSurvey());
            SurveyResponse surveyResponse = CreateSurveyResponse();
            _mockSurveyResponseRepository.Setup(m => m.Create(surveyResponse)).Returns(surveyResponse);

        }

        private Survey CreateSurvey()
        {
            var survey = new Survey
            {
                Id = 1,
                SurveySections = new List<SurveySection>
                {
                    new SurveySection
                    {
                        Id = 1,
                        SectionName = "Sekcija 1",
                        IsDoctorSection = false,
                        SurveyQuestions = new List<SurveyQuestion>
                        {
                            new SurveyQuestion
                            {
                                Id = 1,
                                Question = "Lorem ipsum?"
                            },
                            new SurveyQuestion
                            {
                                Id = 2,
                                Question = "dolor sit amet?"
                            }
                        }
                    },
                    new SurveySection
                    {
                        Id = 2,
                        SectionName = "Sekcija 2",
                        IsDoctorSection = true,
                        SurveyQuestions = new List<SurveyQuestion>()
                        {
                            new SurveyQuestion
                            {
                                Id = 3,
                                Question = "consectetur adipiscing?"
                            },
                            new SurveyQuestion
                            {
                                Id = 4,
                                Question = "Quisque vitae?"
                            }
                        }
                    }
                }
            };
            return survey;
        }

        private SurveyResponse CreateSurveyResponse()
        {
            return new SurveyResponse
            {
                Id = 1,
                SurveyId = 1
            };
        }
    }
}
