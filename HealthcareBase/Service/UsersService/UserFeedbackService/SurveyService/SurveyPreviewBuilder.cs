using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;

namespace Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyPreviewBuilder
    {
        private readonly SurveyDTO surveyDto = new SurveyDTO();
        private readonly IRatedSectionService ratedSectionService;
        private readonly IRatedQuestionService ratedQuestionService;
        private readonly ISurveyService surveyService;

        public SurveyPreviewBuilder(IRatedSectionService ratedSectionService,
            IRatedQuestionService ratedQuestionService,
            ISurveyService surveyService)
        {
            this.ratedSectionService = ratedSectionService;
            this.ratedQuestionService = ratedQuestionService;
            this.surveyService = surveyService;
        }

        public SurveyDTO Build(int surveyId)
        {
            var survey = surveyService.GetById(surveyId);
            surveyDto.SurveyId = surveyId;
            surveyDto.SurveySections = BuildSurveySections(survey.SurveySections);
            return surveyDto;
        }

        private List<SurveySectionDTO> BuildSurveySections(IEnumerable<SurveySection> surveySections)
        {
            return surveySections
                .Select(surveySection => BuildSurveyQuestionDto(surveySection))
                .ToList();
        }

        private SurveySectionDTO BuildSurveyQuestionDto(SurveySection surveySection)
        {
            return new SurveySectionDTO
            {
                SectionName = surveySection.SectionName,
                SectionId = surveySection.Id,
                AverageRating = ratedSectionService.GetSectionAverage(surveySection.Id),
                SurveyQuestions = BuildSurveyQuestions(surveySection.SurveyQuestions),
            };
        }

        private List<SurveyQuestionDTO> BuildSurveyQuestions(IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildSurveyQuestionDto(surveyQuestion))
                .ToList();
        }

        private SurveyQuestionDTO BuildSurveyQuestionDto(SurveyQuestion surveyQuestion)
        {
            return new SurveyQuestionDTO
            {
                QuestionId = surveyQuestion.Id,
                QuestionAverage = ratedQuestionService.GetQuestionAverage(surveyQuestion.Id),
                RatingsCount = ratedQuestionService.GetRatingsCount(surveyQuestion.Id),
                Question = surveyQuestion.Question
            };
        }
        
    }
}