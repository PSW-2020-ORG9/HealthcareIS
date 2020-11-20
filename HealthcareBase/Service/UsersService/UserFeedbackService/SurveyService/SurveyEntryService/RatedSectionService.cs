using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository;
using Repository.Generics;

namespace Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService
{
    public class RatedSectionService:IRatedSectionService
    {
        private readonly RepositoryWrapper<RatedSectionRepository> ratedSectionRepository;

        public RatedSectionService(RatedSectionRepository repository)
        {
            ratedSectionRepository = new RepositoryWrapper<RatedSectionRepository>(repository);
        }

        public double GetSectionAverage(int surveySectionId)
        {
            var surveySection = ratedSectionRepository.Repository
                .GetMatching(s => s.SurveySectionId == surveySectionId).First();

            return surveySection.RatedSurveyQuestions
                .Average(x => x.Rating);


        }
       
    }
}