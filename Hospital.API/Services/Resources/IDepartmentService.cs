using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Hospital.API.Services.Resources
{
    public interface IDepartmentService
    {
        Department GetByID(int id);
        IEnumerable<Department> GetAll();
        Department Create(Department department);
        Department Update(Department department);
        void Delete(Department department);
    }
}
