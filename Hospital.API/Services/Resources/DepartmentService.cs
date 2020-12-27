using General.Repository;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
{
    public class DepartmentService : IDepartmentService
    {
        private readonly RepositoryWrapper<IDepartmentRepository> departmentRepository;
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IRoomRepository roomRepository)
        {
            this.departmentRepository = new RepositoryWrapper<IDepartmentRepository>(departmentRepository);
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
        }

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = new RepositoryWrapper<IDepartmentRepository>(departmentRepository);
        }

        public Department GetByID(int id)
        {
            return departmentRepository.Repository.GetByID(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return departmentRepository.Repository.GetAll();
        }

        public Department Create(Department department)
        {
            if (department is null)
                throw new ArgumentException();
            return departmentRepository.Repository.Create(department);
        }

        public Department Update(Department department)
        {
            if (department is null)
                throw new ArgumentException();
            return departmentRepository.Repository.Update(department);
        }

        public void Delete(Department department)
        {
            if (department is null)
                throw new ArgumentException();
            DeleteFromRooms(department);
            DeleteFromHospitalizationTypes(department);
            departmentRepository.Repository.Delete(department);
        }

        private void DeleteFromRooms(Department department)
        {
            var roomsInDepartment = roomRepository.Repository.GetByDepartment(department);
            foreach (var room in roomsInDepartment)
            {
                room.Department = null;
                roomRepository.Repository.Update(room);
            }
        }

        private void DeleteFromHospitalizationTypes(Department department)
        {
            throw new NotImplementedException();
        }
    }
}