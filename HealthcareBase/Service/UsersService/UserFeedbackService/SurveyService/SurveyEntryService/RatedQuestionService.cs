using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository;
using Repository.Generics;

namespace Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService
{
    public class RatedQuestionService:IRatedQuestionService
    {
        private readonly RepositoryWrapper<RatedQuestionRepository> ratedQuestionRepository;

        
        public RatedQuestionService(RatedQuestionRepository repository)
        {
            ratedQuestionRepository = new RepositoryWrapper<RatedQuestionRepository>(repository);
        }

        public double GetQuestionAverage(int surveyQuestionId)
        {
            return ratedQuestionRepository.Repository
                .GetMatching(surveyQuestion => surveyQuestion.SurveyQuestionId == surveyQuestionId)
                .Average(surveyQuestion => surveyQuestion.Rating);
        }

        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId)
        {
            var surveyQuestions = ratedQuestionRepository.Repository
                .GetMatching(surveyQuestion => surveyQuestion.SurveyQuestionId == surveyQuestionId);
            
            return FillRatings(surveyQuestions, InitRatingsCount());
        }

        private static Dictionary<int, int> FillRatings(IEnumerable<RatedSurveyQuestion> surveyQuestions, Dictionary<int, int> ratingsCount)
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
    }
}