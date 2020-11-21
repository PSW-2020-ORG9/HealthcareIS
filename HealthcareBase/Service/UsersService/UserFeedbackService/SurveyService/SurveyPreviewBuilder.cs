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

        public SurveyPreviewBuilder(IRatedSectionService ratedSectionService,
            ISurveyService surveyService,
            IDoctorService doctorService)
        {
            this.ratedSectionService = ratedSectionService;
            this.surveyService = surveyService;
            this.doctorService = doctorService;
        }
        

        public SurveyDTO Build(int surveyId)
        {
            var survey = surveyService.GetById(surveyId);
            surveyDto.SurveyId = surveyId;
            surveyDto.SurveySections = BuildSurveySections(survey.SurveySections);
            surveyDto.DoctorSurveySections = BuildDoctorSurveySections(survey.SurveySections);
            return surveyDto;
        }

        private List<DoctorSurveySectionDTO> BuildDoctorSurveySections(IEnumerable<SurveySection> surveySections)
        {
            var doctors = doctorService.GetAll();
            var doctorSurveySection = surveySections.First(s => s.IsDoctorSection);
            return doctors
                    .Select(doctor => BuildDoctorSurveySectionDto(doctor, doctorSurveySection))
                    .ToList();
        }

        private DoctorSurveySectionDTO BuildDoctorSurveySectionDto(Employee doctor, SurveySection doctorSurveySection)
        {
            
            var doctorSurveySectionDto = new DoctorSurveySectionDTO
            {
                DoctorName = doctor.Name,
                AverageRating = ratedSectionService
                    .GetDoctorSectionAverage(doctorSurveySection.Id,doctor.EmployeeID),
                SectionId = doctorSurveySection.Id,
                SectionName = doctorSurveySection.SectionName,
                SurveyQuestions = BuildDoctorSurveyQuestions(doctor.EmployeeID,doctorSurveySection.SurveyQuestions)
            };
            return doctorSurveySectionDto;
        }

        private List<SurveyQuestionDTO> BuildDoctorSurveyQuestions(int doctorId, IEnumerable<SurveyQuestion> surveyQuestions)
        {
            return surveyQuestions
                .Select(surveyQuestion => BuildDoctorSurveyQuestionDto(doctorId,surveyQuestion))
                .ToList();
        }

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
                QuestionAverage = ratedSectionService.GetQuestionAverage(surveyQuestion.Id),
                RatingsCount = ratedSectionService.GetRatingsCount(surveyQuestion.Id),
                Question = surveyQuestion.Question
            };
        }
        
    }
}