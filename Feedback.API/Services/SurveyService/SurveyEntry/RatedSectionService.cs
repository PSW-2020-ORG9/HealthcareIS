using Feedback.API.Infrastructure.Repositories;
using Feedback.API.Infrastructure.Repositories.SurveyEntries;
using Feedback.API.Model.Survey.SurveyEntry;
using General.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.API.Services.SurveyEntry
{
    public class RatedSectionService : IRatedSectionService
    {
        private readonly RepositoryWrapper<IRatedSectionRepository> ratedSectionRepository;

        public RatedSectionService(IRatedSectionRepository repository)
        {
            ratedSectionRepository = new RepositoryWrapper<IRatedSectionRepository>(repository);
        }
        /// <summary>
        /// Gets average survey section rating
        /// </summary>
        /// <param name="surveySectionId"></param>
        /// <returns></returns>
        public double GetSectionAverage(int surveySectionId)
        {
            return FindAverage(GetBySectionId(surveySectionId));
        }
        /// <summary>
        /// Finds average survey section rating.
        /// </summary>
        /// <param name="surveySections">A list of rated survey sections.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets all the rated survey sections with given id.
        /// </summary>
        /// <param name="surveySectionId"></param>
        /// <returns></returns>
        private IEnumerable<RatedSurveySection> GetBySectionId(int surveySectionId)
        {
            return ratedSectionRepository.Repository
                .GetMatching(s => s.SurveySectionId == surveySectionId);
        }
        /// <summary>
        /// Gets average doctor survey section rating of 
        /// </summary>
        /// <param name="surveySectionId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public double GetDoctorSectionAverage(int surveySectionId, string doctorId)
        {
            return FindAverage(FilterByDoctorId(doctorId, GetBySectionId(surveySectionId)));
        }

        /// <summary>
        /// Calculates average rating for a question in doctor section
        /// </summary>
        /// <param name="surveyQuestionId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public double GetDoctorQuestionAverage(int surveyQuestionId, string doctorId)
        {
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,
                FilterByDoctorId(doctorId, ratedSectionRepository.Repository.GetAll()));
            if(surveyQuestions.Count!=0)
                return surveyQuestions.Average(sq => sq.Rating);
            return 0;
        }
        /// <summary>
        /// Calculates average rating for a question
        /// </summary>
        /// <param name="surveyQuestionId"></param>
        /// <returns></returns>
        public double GetQuestionAverage(int surveyQuestionId)
        {
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,
                    ratedSectionRepository.Repository.GetAll());
            if(surveyQuestions.Count!=0)
                return surveyQuestions.Average(sq => sq.Rating);
            return 0;

        }

        /// <summary>
        /// Gets a dictionary of ratings for a doctor survey question
        /// </summary>
        /// <param name="surveyQuestionId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetDoctorsRatingsCount(int surveyQuestionId, string doctorId)
        {
            var surveySections = FilterByDoctorId(doctorId, ratedSectionRepository.Repository.GetAll());
            var surveyQuestions = FilterBySurveyQuestionId(surveyQuestionId,surveySections);
            return FillRatings(surveyQuestions, InitRatingsCount());
        }
        /// <summary>
        /// Gets a dictionary of ratings for a survey question
        /// </summary>
        /// <param name="surveyQuestionId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId)
        {
            var surveySections = ratedSectionRepository.Repository.GetAll();
            var surveyQuestions =  FilterBySurveyQuestionId(surveyQuestionId,surveySections);
            return FillRatings(surveyQuestions, InitRatingsCount());
        }
        /// <summary>
        /// Filters passed collection of rated survey sections with a given survey section id.
        /// </summary>
        /// <param name="surveyQuestionId"></param>
        /// <param name="surveySections"></param>
        /// <returns></returns>
        private static List<RatedSurveyQuestion> FilterBySurveyQuestionId(int surveyQuestionId,
            IEnumerable<RatedSurveySection> surveySections)
        {
            var surveyQuestions = new List<RatedSurveyQuestion>();
            foreach (var surveySection in surveySections)
                surveyQuestions.AddRange(
                    surveySection.RatedSurveyQuestions.Where(surveyQuestion => surveyQuestion.SurveyQuestionId == surveyQuestionId));

            return surveyQuestions;
        }

        /// <summary>
        /// Fills passed dictionary with ratings
        /// </summary>
        /// <param name="surveyQuestions"></param>
        /// <param name="ratingsCount"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Filters a given collection by a doctor id.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="surveySections"></param>
        /// <returns></returns>
        private static IEnumerable<DoctorSurveySection> FilterByDoctorId(string doctorId,
            IEnumerable<RatedSurveySection> surveySections)
        {
            var doctorSurveySections = new List<DoctorSurveySection>();
            foreach (var surveySection in surveySections)
            {
                if (!(surveySection is DoctorSurveySection section)) continue;
                if (section.Doctor.Id == doctorId)
                    doctorSurveySections.Add(section);
            }

            return doctorSurveySections;
        }
       
        
        
    }
}