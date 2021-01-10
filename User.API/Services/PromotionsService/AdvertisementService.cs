using System.Collections.Generic;
using General.Repository;
using User.API.Infrastructure.Repositories.Promotions.Interfaces;
using User.API.Model.Promotions;

namespace User.API.Services.PromotionsService
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly RepositoryWrapper<IAdvertisementRepository> advertisementRepository;

        public AdvertisementService(IAdvertisementRepository repository)
        {
            advertisementRepository = new RepositoryWrapper<IAdvertisementRepository>(repository);
        }

        public IEnumerable<Advertisement> GetAllActive() 
            => advertisementRepository.Repository.GetAllActive();
    }
}