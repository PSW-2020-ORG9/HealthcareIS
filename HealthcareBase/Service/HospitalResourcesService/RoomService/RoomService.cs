// File:    RoomService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class RoomService

using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Service.ScheduleService;
using HealthcareBase.Service.ScheduleService.AvailabilityCalculators;

namespace HealthcareBase.Service.HospitalResourcesService.RoomService
{
    public class RoomService
    {
        private readonly CurrentScheduleContext currentScheduleContext;
        private readonly RepositoryWrapper<IDepartmentRepository> departmentRepository;
        private readonly RepositoryWrapper<IEquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<IExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<IHospitalizationRepository> hospitalizationRepository;
        private readonly RepositoryWrapper<IRenovationRepository> renovationRepository;
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;
        private readonly RepositoryWrapper<ISurgeryRepository> surgeryRepository;

        public RoomService(
            IRoomRepository roomRepository,
            IRenovationRepository renovationRepository,
            IEquipmentUnitRepository equipmentUnitRepository,
            IDepartmentRepository departmentRepository,
            CurrentScheduleContext currentScheduleContext,
            IExaminationRepository examinationRepository,
            ISurgeryRepository surgeryRepository,
            IHospitalizationRepository hospitalizationRepository)
        {
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
            this.renovationRepository = new RepositoryWrapper<IRenovationRepository>(renovationRepository);
            this.equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
            this.departmentRepository = new RepositoryWrapper<IDepartmentRepository>(departmentRepository);
            this.currentScheduleContext = currentScheduleContext;
            this.examinationRepository = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this.surgeryRepository = new RepositoryWrapper<ISurgeryRepository>(surgeryRepository);
            this.hospitalizationRepository =
                new RepositoryWrapper<IHospitalizationRepository>(hospitalizationRepository);
        }

        public RoomAvailabilityDTO GetRoomAvailability(Room room, TimeInterval time)
        {
            RoomAvailabilityCalculator calculator = new ConsiderProceduresInRoomCalculator(
                new ConsiderHospitalizationsInRoomCalculator(new ConsiderRenovationsCalculator()));

            var initialAvailability = new RoomAvailabilityDTO
            {
                Room = room,
                Availability = new TimeIntervalCollection(time)
            };

            return calculator.Calculate(initialAvailability, currentScheduleContext);
        }

        public IEnumerable<Room> GetAppropriate(HospitalizationType hospitalizationType)
        {
            var appropriate = new List<Room>();
            foreach (var room in roomRepository.Repository.GetByEquipment(hospitalizationType.NecessaryEquipment))
            {
                if (room.Purpose != RoomType.recoveryRoom)
                    continue;
                if (room.Department is null)
                    continue;
                //if (!hospitalizationType.AppropriateDepartments.Contains(room.Department))
                  //  continue;

                appropriate.Add(room);
            }

            return appropriate;
        }

        public IEnumerable<Room> GetByEquipment(IEnumerable<EquipmentType> equipment)
        {
            return roomRepository.Repository.GetByEquipment(equipment);
        }

        public Room GetByID(int id)
        {
            return roomRepository.Repository.GetByID(id);
        }

        public IEnumerable<Room> GetAll()
        {
            return roomRepository.Repository.GetAll();
        }

        public Room Create(Room room)
        {
            if (room is null)
                throw new BadRequestException();
            if (room.Department != null)
                room.Department = departmentRepository.Repository.GetByID(room.Department.GetKey());
            return roomRepository.Repository.Create(room);
        }

        public Room Update(Room room)
        {
            if (room is null)
                throw new BadRequestException();
            if (!IsCurrentlyInRenovation(room))
                throw new ScheduleViolationException();
            return roomRepository.Repository.Update(room);
        }

        public bool IsCurrentlyInRenovation(Room room)
        {
            var now = new TimeInterval
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(5)
            };
            var renovations = renovationRepository.Repository.getByRoomAndTime(room, now);
            if (renovations.Count() == 0)
                return false;
            return true;
        }

        public void Delete(Room room)
        {
            if (room is null)
                throw new BadRequestException();
            if (!IsCurrentlyInRenovation(room))
                throw new ScheduleViolationException();
            DeleteFromHospitalizations(room);
            DeleteFromExaminations(room);
            DeleteFromSurgeries(room);
            DeleteRenovationsByRoom(room);
            roomRepository.Repository.Delete(room);
        }

        private void DeleteFromHospitalizations(Room room)
        {
            foreach (var hospitalization in hospitalizationRepository.Repository.GetAll())
                if (room.Equals(hospitalization.Room))
                {
                    hospitalization.Room = null;
                    hospitalizationRepository.Repository.Update(hospitalization);
                }
        }

        private void DeleteFromExaminations(Room room)
        {
            foreach (var examination in examinationRepository.Repository.GetAll())
                if (room.Equals(examination.Room))
                {
                    examination.Room = null;
                    examinationRepository.Repository.Update(examination);
                }
        }

        private void DeleteFromSurgeries(Room room)
        {
            foreach (var surgery in surgeryRepository.Repository.GetAll())
                if (room.Equals(surgery.Room))
                {
                    surgery.Room = null;
                    surgeryRepository.Repository.Update(surgery);
                }
        }

        private void DeleteRenovationsByRoom(Room room)
        {
            foreach (var renovation in renovationRepository.Repository.GetAll())
                if (room.Equals(renovation.Room))
                    renovationRepository.Repository.Delete(renovation);
        }

        public Room MoveEquipmentToRoom(Room room, EquipmentUnit equipmentUnit)
        {
            if (room is null || equipmentUnit is null || equipmentUnit.EquipmentType is null)
                throw new BadRequestException();
            if (equipmentUnit.EquipmentType.RequiresRenovationToMove && !IsCurrentlyInRenovation(room))
                throw new ScheduleViolationException();

            equipmentUnit = equipmentUnitRepository.Repository.GetByID(equipmentUnit.GetKey());
            room = roomRepository.Repository.GetByID(room.GetKey());

            equipmentUnit.CurrentLocation = room;
            room.AddEquipment(equipmentUnit);
            equipmentUnitRepository.Repository.Update(equipmentUnit);

            return room;
        }
    }
}