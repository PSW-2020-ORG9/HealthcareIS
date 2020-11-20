using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using Moq;
using Newtonsoft.Json;
using Service.UsersService.UserFeedbackService.SurveyService;
using Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;
using Shouldly;
using Xunit;

namespace HealthcareBaseTests
{
    public class PatientSurveyTests
    {
        private readonly Mock<ISurveyService> mockSurveyService = new Mock<ISurveyService>();
        private readonly Mock<IRatedQuestionService> mockRatedQuestionService = new Mock<IRatedQuestionService>();
        private readonly Mock<IRatedSectionService> mockRatedSectionService = new Mock<IRatedSectionService>();
        private readonly SurveyPreviewBuilder surveyPreviewBuilder;
        
        public PatientSurveyTests()
        {
            surveyPreviewBuilder = new SurveyPreviewBuilder(mockRatedSectionService.Object,
                                                            mockRatedQuestionService.Object,
                                                            mockSurveyService.Object);
        }
        [Fact]
        public void Finds_average_survey_question_rating()
        {
            var ratedQuestionService =
                new RatedQuestionService(CreateSurveyQuestionStubRepository().Object);

            Assert.Equal(CreateRatingsCount(),
                ratedQuestionService.GetRatingsCount(1));
        }
        [Fact]
        private void Checks_survey_preview_build()
        {
            
            SetupSurveyServices();
            var surveyJson1 = JsonConvert.SerializeObject(CreateSurveyDto());
            var surveyJson2 = JsonConvert.SerializeObject(surveyPreviewBuilder.Build(1));
            surveyJson1.ShouldBe(surveyJson2);
        }

        private static Dictionary<int, int> CreateRatingsCount()
        {
            var ratingsCount = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 0},
                {4, 1},
                {5, 3}
            };
            return ratingsCount;
        }

        private static Mock<RatedQuestionRepository> CreateSurveyQuestionStubRepository()
        {
            var stubRepository = new Mock<RatedQuestionRepository>();
            stubRepository.Setup(m => m
                    .GetMatching(It.IsAny<Expression<Func<RatedSurveyQuestion, bool>>>()))
                    .Returns(PopulateRatedSurveyQuestions());
            return stubRepository;
        }

        private static IEnumerable<RatedSurveyQuestion> PopulateRatedSurveyQuestions()
        {
            var ratedSurveyQuestions = new List<RatedSurveyQuestion>
            {
                new RatedSurveyQuestion {Id = 1, Rating = 2},
                new RatedSurveyQuestion {Id = 1, Rating = 4},
                new RatedSurveyQuestion {Id = 1, Rating = 5},
                new RatedSurveyQuestion {Id = 1, Rating = 5},
                new RatedSurveyQuestion {Id = 1, Rating = 5}
            };
            return ratedSurveyQuestions;
        }

        

        private void SetupSurveyServices()
        {
            mockSurveyService.Setup(m => m.GetById(It.IsAny<int>())).Returns(CreateSurvey());

            mockRatedQuestionService.Setup(m => m
                    .GetQuestionAverage(It.IsAny<int>()))
                .Returns(3.45);
            mockRatedQuestionService.Setup(m => m
                    .GetRatingsCount(It.IsAny<int>()))
                .Returns(CreateRatingsCount);
            mockRatedSectionService.Setup(m => m
                    .GetSectionAverage(It.IsAny<int>()))
                .Returns(3.5);
        }

        private SurveyDTO CreateSurveyDto()
        {
            var surveyDto = new SurveyDTO
            {
                SurveyId = 1,
                SurveySections = new List<SurveySectionDTO>
                {
                    new SurveySectionDTO
                    {
                        SectionId = 1,
                        SectionName = "Sekcija 1",
                        AverageRating = 3.5,
                        SurveyQuestions = new List<SurveyQuestionDTO>
                        {
                            new SurveyQuestionDTO
                            {
                                Question = "Lorem ipsum?",
                                QuestionAverage = 3.45,
                                QuestionId = 1,
                                RatingsCount = CreateRatingsCount()
                            },
                            new SurveyQuestionDTO
                            {
                                Question = "dolor sit amet?",
                                QuestionAverage = 3.45,
                                QuestionId = 2,
                                RatingsCount = CreateRatingsCount()
                            },
                        }
                    },
                    new SurveySectionDTO
                    {
                        SectionId = 1,
                        SectionName = "Sekcija 2",
                        AverageRating = 3.5,
                        SurveyQuestions = new List<SurveyQuestionDTO>
                        {
                            new SurveyQuestionDTO
                            {
                                Question = "consectetur adipiscing?",
                                QuestionAverage = 3.45,
                                QuestionId = 3,
                                RatingsCount = CreateRatingsCount()
                            },
                            new SurveyQuestionDTO
                            {
                                Question = "Quisque vitae?",
                                QuestionAverage = 3.45,
                                QuestionId = 4,
                                RatingsCount = CreateRatingsCount()
                            },
                        }
                    }
                }
            };
            return surveyDto;
        }

        private static Survey CreateSurvey()
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
                        Id = 1,
                        SectionName = "Sekcija 2",
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
    }
}