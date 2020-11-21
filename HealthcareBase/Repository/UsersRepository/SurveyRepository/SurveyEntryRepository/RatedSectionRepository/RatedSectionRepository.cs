using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository
{
    public interface RatedSectionRepository : IWrappableRepository<RatedSurveySection, int>
    {
    }
}