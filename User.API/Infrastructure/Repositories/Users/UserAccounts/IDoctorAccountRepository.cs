using General.Repository;
using User.API.Model.Users.Employees.Doctors;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public interface IDoctorAccountRepository : IWrappableRepository<DoctorAccount, int>
    {
        
    }
}