using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository;
using HealthcareBase.Service.UsersService.EmployeeService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace HealthcareBaseTests
{
    public class PatientSurveyTests
    {
        private readonly Mock<ISurveyService> mockSurveyService = new Mock<ISurveyService>();
        private readonly Mock<IRatedSectionService> mockRatedSectionService = new Mock<IRatedSectionService>();
        private readonly Mock<IDoctorService> mockDoctorService = new Mock<IDoctorService>();
        private readonly SurveyPreviewBuilder surveyPreviewBuilder;
        
        
        public PatientSurveyTests()
        {
            surveyPreviewBuilder = new SurveyPreviewBuilder(mockRatedSectionService.Object,
                                                            mockSurveyService.Object,
                                                            mockDoctorService.Object);
        }
        [Fact]
        private void Checks_survey_preview_build()
        {
            
            SetupSurveyServices();
            var surveyJson1 = JsonConvert.SerializeObject(CreateSurveyDto());
            var surveyJson2 = JsonConvert.SerializeObject(surveyPreviewBuilder.Build(1));
            surveyJson2.ShouldBe(surveyJson1);
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
            mockSurveyService
                .Setup(m => m.GetById(It.IsAny<int>())).Returns(CreateSurvey());

            mockRatedSectionService.Setup(m => m
                .GetSectionAverage(It.IsAny<int>()))
                .Returns(3.5);
            mockRatedSectionService.Setup(m => m
                .GetDoctorSectionAverage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(3.5);
            mockRatedSectionService.Setup(m => m
                .GetQuestionAverage(It.IsAny<int>()))
                .Returns(3.45);
            mockRatedSectionService.Setup(m => m
                .GetDoctorQuestionAverage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(3.45);
            mockRatedSectionService.Setup(m => m
                .GetRatingsCount(It.IsAny<int>()))
                .Returns(CreateRatingsCount());
            mockRatedSectionService.Setup(m => m
                    .GetDoctorsRatingsCount(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(CreateRatingsCount());
            mockDoctorService.Setup(m => m.GetAll()).Returns(createDoctors());
        }

        private SurveyDTO CreateSurveyDto()
        {
            return new SurveyDTO
            {
                SurveyId = 1,
                DoctorSurveySections = new List<DoctorSurveySectionDTO>
                {
                    new DoctorSurveySectionDTO
                    {
                        SectionId = 2,
                        SectionName = "Sekcija 2",
                        DoctorName = "doktor1",
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
                    },
                    new DoctorSurveySectionDTO
                    {
                        SectionId = 2,
                        SectionName = "Sekcija 2",
                        DoctorName = "doktor2",
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
                },
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
                    }
                }
            };
            
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

        private List<Doctor> createDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    Id = 1,
                    Person = new Person
                    {
                        Name = "doktor1"
                    }
                },
                new Doctor
                {
                    Id = 1,
                    Person = new Person
                    {
                        Name = "doktor2"
                    }
                }
            };
        }
    }
}