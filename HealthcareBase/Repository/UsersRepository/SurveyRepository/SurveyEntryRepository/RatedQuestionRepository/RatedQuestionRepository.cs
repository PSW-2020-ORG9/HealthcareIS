using HealthcareBase.Model.Users.Survey.SurveyEntry;
using Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository
{
    public interface RatedQuestionRepository:IWrappableRepository<RatedSurveyQuestion,int>
    {
        
    }
}