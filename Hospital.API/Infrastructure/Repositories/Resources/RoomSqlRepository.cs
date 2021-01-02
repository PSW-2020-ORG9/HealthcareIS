using General;
using General.Repository;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public class RoomSqlRepository : GenericSqlRepository<Room, int>, IRoomRepository
    {
        public RoomSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }
        public IEnumerable<Room> GetByDepartment(Department department)
            => GetMatching(r => r.DepartmentId == department.Id);
    }
}
