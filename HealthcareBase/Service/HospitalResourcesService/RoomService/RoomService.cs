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
        private readonly DepartmentRepository departmentRepository;
        private readonly EquipmentUnitRepository equipmentUnitRepository;
        private readonly ExaminationRepository examinationRepository;
        private readonly HospitalizationRepository hospitalizationRepository;
        private readonly RenovationRepository renovationRepository;
        private readonly RoomRepository roomRepository;
        private readonly SurgeryRepository surgeryRepository;

        public RoomService(RoomRepository roomRepository, RenovationRepository renovationRepository,
            EquipmentUnitRepository equipmentUnitRepository, DepartmentRepository departmentRepository,
            CurrentScheduleContext currentScheduleContext, ExaminationRepository examinationRepository,
            SurgeryRepository surgeryRepository, HospitalizationRepository hospitalizationRepository)
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
            foreach (var room in roomRepository.GetByEquipment(procedureType.NecessaryEquipment))
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
            foreach (var room in roomRepository.GetByEquipment(hospitalizationType.NecessaryEquipment))
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
            return roomRepository.GetByEquipment(equipment);
        }

        public Room GetByID(int id)
        {
            return roomRepository.GetByID(id);
        }

        public IEnumerable<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        public Room Create(Room room)
        {
            if (room is null)
                throw new BadRequestException();
            if (room.Department != null)
                room.Department = departmentRepository.GetByID(room.Department.GetKey());
            return roomRepository.Create(room);
        }

        public Room Update(Room room)
        {
            if (room is null)
                throw new BadRequestException();
            if (!IsCurrentlyInRenovation(room))
                throw new ScheduleViolationException();
            return roomRepository.Update(room);
        }

        public bool IsCurrentlyInRenovation(Room room)
        {
            var now = new TimeInterval
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(5)
            };
            var renovations = renovationRepository.getByRoomAndTime(room, now);
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
            roomRepository.Delete(room);
        }

        private void DeleteFromHospitalizations(Room room)
        {
            foreach (var hospitalization in hospitalizationRepository.GetAll())
                if (room.Equals(hospitalization.Room))
                {
                    hospitalization.Room = null;
                    hospitalizationRepository.Update(hospitalization);
                }
        }

        private void DeleteFromExaminations(Room room)
        {
            foreach (var examination in examinationRepository.GetAll())
                if (room.Equals(examination.Room))
                {
                    examination.Room = null;
                    examinationRepository.Update(examination);
                }
        }

        private void DeleteFromSurgeries(Room room)
        {
            foreach (var surgery in surgeryRepository.GetAll())
                if (room.Equals(surgery.Room))
                {
                    surgery.Room = null;
                    surgeryRepository.Update(surgery);
                }
        }

        private void DeleteRenovationsByRoom(Room room)
        {
            foreach (var renovation in renovationRepository.GetAll())
                if (room.Equals(renovation.Room))
                    renovationRepository.Delete(renovation);
        }

        public Room MoveEquipmentToRoom(Room room, EquipmentUnit equipmentUnit)
        {
            if (room is null || equipmentUnit is null || equipmentUnit.EquipmentType is null)
                throw new BadRequestException();
            if (equipmentUnit.EquipmentType.RequiresRenovationToMove && !IsCurrentlyInRenovation(room))
                throw new ScheduleViolationException();

            equipmentUnit = equipmentUnitRepository.GetByID(equipmentUnit.GetKey());
            room = roomRepository.GetByID(room.GetKey());

            equipmentUnit.CurrentLocation = room;
            room.AddEquipment(equipmentUnit);
            equipmentUnitRepository.Update(equipmentUnit);

            return room;
        }
    }
}