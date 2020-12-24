using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Service.UsersService.EmployeeService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyPreviewBuilder
    {
        private readonly SurveyDTO surveyDto = new SurveyDTO();
        private readonly IRatedSectionService ratedSectionService;
        private readonly ISurveyService surveyService;
        private readonly IDoctorService doctorService;

        public SurveyPreviewBuilder(ISurveyService surveyService,IRatedSectionService sectionService,
            IDoctorService doctorService)
        {
            this.ratedSectionService = sectionService;
            this.surveyService = surveyService;
            this.doctorService = doctorService;
        }
        
        /// <summary>
        /// Constructs SurveyDto object 
        /// </summary>
        /// <param name="surveyId">Predefined survey id.</param>
        /// <returns></returns>
        public SurveyDTO Build(int surveyId)
        {
            var survey = surveyService.GetById(surveyId);
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
                AverageRating = ratedSectionService.GetSectionAverage(surveySection.Id),
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
            var doctors = doctorService.GetAll();
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
        private DoctorSurveySectionDTO BuildDoctorSurveySectionDto(Employee doctor, SurveySection doctorSurveySection)
        {
            
            var doctorSurveySectionDto = new DoctorSurveySectionDTO
            {
                DoctorName = doctor.Person.Name,
                AverageRating = ratedSectionService
                    .GetDoctorSectionAverage(doctorSurveySection.Id,doctor.Id),
                SectionId = doctorSurveySection.Id,
                SectionName = doctorSurveySection.SectionName,
                SurveyQuestions = BuildDoctorSurveyQuestions(doctor.Id,doctorSurveySection.SurveyQuestions)
            };
            return doctorSurveySectionDto;
        }

        /// <summary>
        /// Constructs List of doctor survey question DTO objects.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="surveyQuestions"> A list of predefined survey questions. </param>
        /// <returns></returns>
        private List<SurveyQuestionDTO> BuildDoctorSurveyQuestions(int doctorId, IEnumerable<SurveyQuestion> surveyQuestions)
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
        private SurveyQuestionDTO BuildDoctorSurveyQuestionDto(int doctorId, SurveyQuestion surveyQuestion)
        {
            return new SurveyQuestionDTO
            {
                QuestionId = surveyQuestion.Id,
                QuestionAverage = ratedSectionService.GetDoctorQuestionAverage(surveyQuestion.Id,doctorId),
                RatingsCount = ratedSectionService.GetDoctorsRatingsCount(surveyQuestion.Id,doctorId),
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
                QuestionAverage = ratedSectionService.GetQuestionAverage(surveyQuestion.Id),
                RatingsCount = ratedSectionService.GetRatingsCount(surveyQuestion.Id),
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