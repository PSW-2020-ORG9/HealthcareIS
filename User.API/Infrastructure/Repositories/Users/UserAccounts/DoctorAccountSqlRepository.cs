using System.Linq;
using General;
using General.Repository;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public class DoctorAccountSqlRepository 
        : GenericSqlRepository<DoctorAccount, int>,
        IDoctorAccountRepository
    {
        public DoctorAccountSqlRepository(IContextFactory factory) : base(factory) {}
    }
}