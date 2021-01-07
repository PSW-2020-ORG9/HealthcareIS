using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
{
    public interface IRoomService
    {
        IEnumerable<Room> GetRoomsByIds(IEnumerable<int> ids);
        Room GetById(int id);
        IEnumerable<Room> getByEquipmentType(string equipmentTypeName);
    }
}
