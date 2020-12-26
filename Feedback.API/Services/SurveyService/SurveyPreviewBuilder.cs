using Feedback.API.Connections;
using Feedback.API.DTOs;
using Feedback.API.Model.Survey;
using Feedback.API.Model.User;
using Feedback.API.Services.SurveyEntry;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.API.Services
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
        
        /// <summary>
        /// Constructs SurveyDto object 
        /// </summary>
        /// <param name="surveyId">Predefined survey id.</param>
        /// <returns></returns>
        public SurveyDTO Build(int surveyId)
        {
            var survey = _surveyService.GetById(surveyId);
            surveyDto.SurveyId = surveyId;
            surveyDto.SurveySections = BuildSurveySections(survey.SurveySections);
            surveyDto.DoctorSurveySections = BuildDoctorSurveySections(survey.SurveySections);
            return surveyDto;
        }
        /// <summary>
        /// Constructs a list of default survey section DTO objects.
        /// </summary>
        /// <param name="surveySections"></param>
        /// <returns></returns>
        private List<SurveySectionDTO> BuildSurveySections(IEnumerable<SurveySection> surveySections)
        {
            return surveySections
                .Where(s=>!s.IsDoctorSection)
                .Select(surveySection => BuildSurveyQuestionDto(surveySection))
                .ToList();
        }
        /// <summary>
        /// Constructs a single, default survey question DTO object.
        /// </summary>
        /// <param name="surveySection"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Constructs List of doctor survey section DTO objects.
        /// </summary>
        /// <param name="surveySections">A list of predefinded survey sections</param>
        /// <returns></returns>
        private List<DoctorSurveySectionDTO> BuildDoctorSurveySections(IEnumerable<SurveySection> surveySections)
        {
            var doctors = _doctorConnection.Get<IEnumerable<Doctor>>();
            var doctorSurveySection = surveySections.First(s => s.IsDoctorSection);
            return doctors
                    .Select(doctor => BuildDoctorSurveySectionDto(doctor, doctorSurveySection))
                    .ToList();
        }
        /// <summary>
        /// Constructs a single doctor survey section DTO object.
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="doctorSurveySection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Constructs List of doctor survey question DTO objects.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="surveyQuestions"> A list of predefined survey questions. </param>
        /// <returns></returns>
        private List<SurveyQuestionDTO> BuildDoctorSurveyQuestions(string doctorId, IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildDoctorSurveyQuestionDto(doctorId,surveyQuestion))
                .ToList();
        }
        /// <summary>
        /// Constructs a single survey section DTO object.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="surveyQuestion"></param>
        /// <returns></returns>
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
        
        

        /// <summary>
        /// Constructs a single survey question DTO object.
        /// </summary>
        /// <param name="surveyQuestion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Constructs a list of survey section DTO objects.
        /// </summary>
        /// <param name="surveyQuestions"></param>
        /// <returns></returns>
        private List<SurveyQuestionDTO> BuildSurveyQuestions(IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildSurveyQuestionDto(surveyQuestion))
                .ToList();
        }
    }
}