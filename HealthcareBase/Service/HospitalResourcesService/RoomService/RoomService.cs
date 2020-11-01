// File:    RoomService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:01
// Purpose: Definition of Class RoomService

using System;
using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Service.ScheduleService;
using Service.ScheduleService.AvailabilityCalculators;

namespace Service.HospitalResourcesService.RoomService
{
    public class RoomService
    {
        private readonly CurrentScheduleContext currentScheduleContext;
        private readonly RepositoryWrapper<DepartmentRepository> departmentRepository;
        private readonly RepositoryWrapper<EquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<ExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<HospitalizationRepository> hospitalizationRepository;
        private readonly RepositoryWrapper<RenovationRepository> renovationRepository;
        private readonly RepositoryWrapper<RoomRepository> roomRepository;
        private readonly RepositoryWrapper<SurgeryRepository> surgeryRepository;

        public RoomService(RepositoryWrapper<RoomRepository> roomRepository,
            RepositoryWrapper<RenovationRepository> renovationRepository,
            RepositoryWrapper<EquipmentUnitRepository> equipmentUnitRepository,
            RepositoryWrapper<DepartmentRepository> departmentRepository,
            CurrentScheduleContext currentScheduleContext,
            RepositoryWrapper<ExaminationRepository> examinationRepository,
            RepositoryWrapper<SurgeryRepository> surgeryRepository,
            RepositoryWrapper<HospitalizationRepository> hospitalizationRepository)
        {
            this.roomRepository = roomRepository;
            this.renovationRepository = renovationRepository;
            this.equipmentUnitRepository = equipmentUnitRepository;
            this.departmentRepository = departmentRepository;
            this.currentScheduleContext = currentScheduleContext;
            this.examinationRepository = examinationRepository;
            this.surgeryRepository = surgeryRepository;
            this.hospitalizationRepository = hospitalizationRepository;
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

        public IEnumerable<Room> GetAppropriate(ProcedureType procedureType)
        {
            var appropriate = new List<Room>();
            foreach (var room in roomRepository.Repository.GetByEquipment(procedureType.NecessaryEquipment))
            {
                if (procedureType.Kind == ProcedureKind.Examination && room.Purpose != RoomType.examinationRoom)
                    continue;
                if (procedureType.Kind == ProcedureKind.Surgery && room.Purpose != RoomType.operatingRoom)
                    continue;

                appropriate.Add(room);
            }

            return appropriate;
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
                if (!hospitalizationType.AppropriateDepartments.Contains(room.Department))
                    continue;

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