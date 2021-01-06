using General.Repository;
using Hospital.API.Model.Resources;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public interface IEquipmentTypeRepository : IWrappableRepository<EquipmentType, int>
    {
    }
}