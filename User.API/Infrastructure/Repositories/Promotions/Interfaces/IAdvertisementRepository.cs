using System.Collections.Generic;
using General.Repository;
using User.API.Model.Promotions;

namespace User.API.Infrastructure.Repositories.Promotions.Interfaces
{
    public interface IAdvertisementRepository : IWrappableRepository<Advertisement,int>
    {
        IEnumerable<Advertisement> GetAllActive();
    }
}