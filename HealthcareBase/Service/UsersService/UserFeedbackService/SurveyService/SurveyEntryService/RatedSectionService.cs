using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService
{
    public class RatedSectionService : IRatedSectionService
    {
        private readonly RepositoryWrapper<RatedSectionRepository> ratedSectionRepository;

        public RatedSectionService(RatedSectionRepository repository)
        {
            ratedSectionRepository = new RepositoryWrapper<RatedSectionRepository>(repository);
        }

        public double GetSectionAverage(int surveySectionId)
        {
            return FindAverage(FilterBySectionId(surveySectionId));
        }

        public double GetDoctorSectionAverage(int surveySectionId, int doctorId)
        {
            return FindAverage(FilterByDoctorId(doctorId, FilterBySectionId(surveySectionId)));
        }


        public double GetDoctorQuestionAverage(int surveyQuestionId, int doctorId)
        {
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,
                FilterByDoctorId(doctorId, ratedSectionRepository.Repository.GetAll()));
            if(surveyQuestions.Count!=0)
                return surveyQuestions.Average(sq => sq.Rating);
            return 0;
        }

        public double GetQuestionAverage(int surveyQuestionId)
        {
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,
                    ratedSectionRepository.Repository.GetAll());
            if(surveyQuestions.Count!=0)
                return surveyQuestions.Average(sq => sq.Rating);
            return 0;

        }

        public Dictionary<int, int> GetDoctorsRatingsCount(int surveyQuestionId, int doctorId)
        {
            var surveySections = FilterByDoctorId(doctorId, ratedSectionRepository.Repository.GetAll());
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,surveySections);
            return FillRatings(surveyQuestions, InitRatingsCount());
        }

        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId)
        {
            var surveySections = ratedSectionRepository.Repository.GetAll();
            var surveyQuestions =  FilterBySurveyQuestionId(surveyQuestionId,surveySections);
            return FillRatings(surveyQuestions, InitRatingsCount());
        }

        private static List<RatedSurveyQuestion> FilterBySurveyQuestionId(int surveyQuestionId,
            IEnumerable<RatedSurveySection> surveySections)
        {
            var surveyQuestions = new List<RatedSurveyQuestion>();
            foreach (var surveySection in surveySections)
                surveyQuestions.AddRange(
                    surveySection.RatedSurveyQuestions.Where(surveyQuestion => surveyQuestion.Id == surveyQuestionId));

            return surveyQuestions;
        }

        private static Dictionary<int, int> FillRatings(IEnumerable<RatedSurveyQuestion> surveyQuestions,
            Dictionary<int, int> ratingsCount)
        {
            foreach (var question in surveyQuestions)
                ratingsCount[question.Rating] += 1;
            return ratingsCount;
        }

        private static Dictionary<int, int> InitRatingsCount()
        {
            var ratingsCount = new Dictionary<int, int>();
            for (var i = RatedSurveyQuestion.MinRating; i <= RatedSurveyQuestion.MaxRating; i++)
                ratingsCount.Add(i, 0);

            return ratingsCount;
        }

        private static IEnumerable<DoctorSurveySection> FilterByDoctorId(int doctorId,
            IEnumerable<RatedSurveySection> surveySections)
        {
            var doctorSurveySections = new List<DoctorSurveySection>();
            foreach (var surveySection in surveySections)
            {
                if (!(surveySection is DoctorSurveySection section)) continue;
                if (section.DoctorId == doctorId)
                    doctorSurveySections.Add(section);
            }

            return doctorSurveySections;
        }

        private static double FindAverage(IEnumerable<RatedSurveySection> surveySections)
        {
            var ratedSurveySections = surveySections.ToList();
            if (ratedSurveySections.Count != 0)
                return ratedSurveySections
                    .Select(ss => ss.RatedSurveyQuestions
                        .Average(x => x.Rating))
                    .ToList()
                    .Average();
            return 0;
        }

        private IEnumerable<RatedSurveySection> FilterBySectionId(int surveySectionId)
        {
            return ratedSectionRepository.Repository
                .GetMatching(s => s.SurveySectionId == surveySectionId);
        }
    }
}