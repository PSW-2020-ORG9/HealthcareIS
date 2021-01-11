using General.Repository;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public interface IUserAccountRepository : IWrappableRepository<UserAccount, int>
    {
    }
}
