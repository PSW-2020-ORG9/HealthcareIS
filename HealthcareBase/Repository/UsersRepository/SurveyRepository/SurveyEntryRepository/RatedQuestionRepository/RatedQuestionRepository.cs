using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository
{
    public interface RatedQuestionRepository:IWrappableRepository<RatedSurveyQuestion,int>
    {
        
    }
}