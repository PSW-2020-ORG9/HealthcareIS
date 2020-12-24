// File:    DepartmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class DepartmentService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Service.HospitalResourcesService.RoomService
{
    public class DepartmentService
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
            throw new NotImplementedException();
        }
    }
}