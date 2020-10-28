// File:    DepartmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class DepartmentService

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.HospitalResourcesService.RoomService
{
    public class DepartmentService
    {
        private DepartmentRepository departmentRepository;
        private RoomRepository roomRepository;
        private HospitalizationTypeRepository hospitalizationTypeRepository;

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
            IEnumerable<Room> roomsInDepartment = roomRepository.GetByDepartment(department);
            foreach (Room room in roomsInDepartment)
            {
                room.Department = null;
                roomRepository.Update(room);
            }
        }

        private void DeleteFromHospitalizationTypes(Department department)
        {
            foreach (HospitalizationType hospitalizationType in hospitalizationTypeRepository.GetAll())
                if (hospitalizationType.AppropriateDepartments.Contains(department))
                {
                    hospitalizationType.RemoveAppropriateDepartments(department);
                    hospitalizationTypeRepository.Update(hospitalizationType);
                }
        }

    }
}