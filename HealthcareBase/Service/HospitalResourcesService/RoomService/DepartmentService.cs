// File:    DepartmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class DepartmentService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.HospitalResourcesService.RoomService
{
    public class DepartmentService
    {
        private readonly RepositoryWrapper<DepartmentRepository> departmentRepository;
        private readonly RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository;
        private readonly RepositoryWrapper<RoomRepository> roomRepository;

        public DepartmentService(RepositoryWrapper<DepartmentRepository> departmentRepository,
            RepositoryWrapper<RoomRepository> roomRepository,
            RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.roomRepository = roomRepository;
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
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
                throw new BadRequestException();
            return departmentRepository.Repository.Create(department);
        }

        public Department Update(Department department)
        {
            if (department is null)
                throw new BadRequestException();
            return departmentRepository.Repository.Update(department);
        }

        public void Delete(Department department)
        {
            if (department is null)
                throw new BadRequestException();
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
            foreach (var hospitalizationType in hospitalizationTypeRepository.Repository.GetAll())
                if (hospitalizationType.AppropriateDepartments.Contains(department))
                {
                    hospitalizationType.RemoveAppropriateDepartments(department);
                    hospitalizationTypeRepository.Repository.Update(hospitalizationType);
                }
        }
    }
}