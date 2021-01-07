using System.Collections.Generic;
using User.API.Model.Promotions;

namespace User.API.Services.PromotionsService
{
    public interface IAdvertisementService
    {
        IEnumerable<Advertisement> GetAllActive();
    }
}