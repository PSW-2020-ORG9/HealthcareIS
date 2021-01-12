using General;
using General.Repository;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public class UserAccountSqlRepository : GenericSqlRepository<UserAccount, int>, IUserAccountRepository
    {
        public UserAccountSqlRepository(IContextFactory factory) : base(factory) { }
    }
}
