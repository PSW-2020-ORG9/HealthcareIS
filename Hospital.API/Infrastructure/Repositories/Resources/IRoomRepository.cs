using General.Repository;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public interface IRoomRepository : IWrappableRepository<Room, int>
    {
        IEnumerable<Room> GetByDepartment(Department department);
    }
}