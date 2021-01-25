using System.Collections.Generic;
using System.Linq;
using Feedback.API.DTOs;
using Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate.User;
using Feedback.API.Model.Survey;
using Feedback.API.Services.SurveyService.SurveyEntry;
using General;

namespace Feedback.API.Services.SurveyService
{
    public class SurveyPreviewBuilder
    {
        private readonly SurveyDTO surveyDto = new SurveyDTO();
        private readonly IRatedSectionService _ratedSectionService;
        private readonly ISurveyService _surveyService;
        private readonly IConnection _doctorConnection;

        public SurveyPreviewBuilder(ISurveyService surveyService, IRatedSectionService sectionService, IConnection doctorConnection)
        {
            _ratedSectionService = sectionService;
            _surveyService = surveyService;
            _doctorConnection = doctorConnection;
        }
        
        public SurveyDTO Build(int surveyId)
        {
            var survey = _surveyService.GetById(surveyId);
            surveyDto.SurveyId = surveyId;
            surveyDto.SurveySections = BuildSurveySections(survey.SurveySections);
            surveyDto.DoctorSurveySections = BuildDoctorSurveySections(survey.SurveySections);
            return surveyDto;
        }
        private List<SurveySectionDTO> BuildSurveySections(IEnumerable<SurveySection> surveySections)
        {
            return surveySections
                .Where(s=>!s.IsDoctorSection)
                .Select(surveySection => BuildSurveyQuestionDto(surveySection))
                .ToList();
        }
        private SurveySectionDTO BuildSurveyQuestionDto(SurveySection surveySection)
        {
            return new SurveySectionDTO
            {
                SectionName = surveySection.SectionName,
                SectionId = surveySection.Id,
                AverageRating = _ratedSectionService.GetSectionAverage(surveySection.Id),
                SurveyQuestions = BuildSurveyQuestions(surveySection.SurveyQuestions),
            };
        }
        private List<DoctorSurveySectionDTO> BuildDoctorSurveySections(IEnumerable<SurveySection> surveySections)
        {
            var doctors = _doctorConnection.Get<IEnumerable<Doctor>>("");
            var doctorSurveySection = surveySections.First(s => s.IsDoctorSection);
            return doctors
                    .Select(doctor => BuildDoctorSurveySectionDto(doctor, doctorSurveySection))
                    .ToList();
        }
        private DoctorSurveySectionDTO BuildDoctorSurveySectionDto(Doctor doctor, SurveySection doctorSurveySection)
        {
            
            var doctorSurveySectionDto = new DoctorSurveySectionDTO
            {
                DoctorName = doctor.Person.Name,
                AverageRating = _ratedSectionService
                    .GetDoctorSectionAverage(doctorSurveySection.Id,doctor.Id),
                SectionId = doctorSurveySection.Id,
                SectionName = doctorSurveySection.SectionName,
                SurveyQuestions = BuildDoctorSurveyQuestions(doctor.Id, doctorSurveySection.SurveyQuestions)
            };
            return doctorSurveySectionDto;
        }
        private List<SurveyQuestionDTO> BuildDoctorSurveyQuestions(string doctorId, IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildDoctorSurveyQuestionDto(doctorId,surveyQuestion))
                .ToList();
        }
        private SurveyQuestionDTO BuildDoctorSurveyQuestionDto(string doctorId, SurveyQuestion surveyQuestion)
        {
            return new SurveyQuestionDTO
            {
                QuestionId = surveyQuestion.Id,
                QuestionAverage = _ratedSectionService.GetDoctorQuestionAverage(surveyQuestion.Id,doctorId),
                RatingsCount = _ratedSectionService.GetDoctorsRatingsCount(surveyQuestion.Id,doctorId),
                Question = surveyQuestion.Question
            };
        }
        
        private SurveyQuestionDTO BuildSurveyQuestionDto(SurveyQuestion surveyQuestion)
        {
            return new SurveyQuestionDTO
            {
                QuestionId = surveyQuestion.Id,
                QuestionAverage = _ratedSectionService.GetQuestionAverage(surveyQuestion.Id),
                RatingsCount = _ratedSectionService.GetRatingsCount(surveyQuestion.Id),
                Question = surveyQuestion.Question
            };
        }
        private List<SurveyQuestionDTO> BuildSurveyQuestions(IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildSurveyQuestionDto(surveyQuestion))
                .ToList();
        }
    }
}