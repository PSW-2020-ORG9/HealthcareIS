// File:    DepartmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class DepartmentService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.HospitalResourcesService.RoomService
{
    public class DepartmentService
    {
        private readonly DepartmentRepository departmentRepository;
        private readonly HospitalizationTypeRepository hospitalizationTypeRepository;
        private readonly RoomRepository roomRepository;

        public DepartmentService(DepartmentRepository departmentRepository, RoomRepository roomRepository,
            HospitalizationTypeRepository hospitalizationTypeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.roomRepository = roomRepository;
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
        }

        public Department GetByID(int id)
        {
            return departmentRepository.GetByID(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return departmentRepository.GetAll();
        }

        public Department Create(Department department)
        {
            if (department is null)
                throw new BadRequestException();
            return departmentRepository.Create(department);
        }

        public Department Update(Department department)
        {
            if (department is null)
                throw new BadRequestException();
            return departmentRepository.Update(department);
        }

        public void Delete(Department department)
        {
            if (department is null)
                throw new BadRequestException();
            DeleteFromRooms(department);
            DeleteFromHospitalizationTypes(department);
            departmentRepository.Delete(department);
        }

        private void DeleteFromRooms(Department department)
        {
            var roomsInDepartment = roomRepository.GetByDepartment(department);
            foreach (var room in roomsInDepartment)
            {
                room.Department = null;
                roomRepository.Update(room);
            }
        }

        private void DeleteFromHospitalizationTypes(Department department)
        {
            foreach (var hospitalizationType in hospitalizationTypeRepository.GetAll())
                if (hospitalizationType.AppropriateDepartments.Contains(department))
                {
                    hospitalizationType.RemoveAppropriateDepartments(department);
                    hospitalizationTypeRepository.Update(hospitalizationType);
                }
        }
    }
}