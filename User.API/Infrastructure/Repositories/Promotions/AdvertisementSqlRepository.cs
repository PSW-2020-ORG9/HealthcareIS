using System.Collections.Generic;
using General;
using General.Repository;
using User.API.Infrastructure.Repositories.Promotions.Interfaces;
using User.API.Model.Promotions;

namespace User.API.Infrastructure.Repositories.Promotions
{
    public class AdvertisementSqlRepository : GenericSqlRepository<Advertisement,int>
                                            , IAdvertisementRepository 
    {
        public AdvertisementSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IEnumerable<Advertisement> GetAllActive() 
            => GetMatching(ad => ad.IsActive);
    }
}