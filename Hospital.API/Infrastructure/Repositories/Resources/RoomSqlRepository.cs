using General;
using General.Repository;
using Hospital.API.Model.Resources;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public class RoomSqlRepository : GenericSqlRepository<Room, int>, IRoomRepository
    {
        public RoomSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }
        public IEnumerable<Room> GetByDepartment(Department department)
            => GetMatching(r => r.DepartmentId == department.Id);

        protected override IQueryable<Room> IncludeFields(IQueryable<Room> query)
        {
            return query.Include(r => r.Department);
        }
    }
}
