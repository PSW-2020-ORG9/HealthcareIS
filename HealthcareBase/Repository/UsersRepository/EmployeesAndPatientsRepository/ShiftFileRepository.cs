// File:    ShiftFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:43:57
// Purpose: Definition of Class ShiftFileRepository

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class ShiftFileRepository : GenericFileRepository<Shift, int>, ShiftRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly RoomRepository roomRepository;

        public ShiftFileRepository(RoomRepository roomRepository, DoctorRepository doctorRepository, string filePath) :
            base(filePath)
        {
            this.roomRepository = roomRepository;
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Shift> GetByDoctor(Doctor doctor)
        {
            var shifts = new List<Shift>();

            foreach (var shift in GetAll())
                if (shift.Doctor.Equals(doctor))
                    shifts.Add(shift);

            return shifts;
        }

        public IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time)
        {
            return GetMatching(shift => shift.Doctor.Equals(doctor) && shift.TimeInterval.Contains(time));
        }

        public IEnumerable<Shift> GetByDoctorAndTimeOverlap(Doctor doctor, TimeInterval time)
        {
            return GetMatching(shift => shift.Doctor.Equals(doctor) && shift.TimeInterval.Overlaps(time));
        }

        protected override int GenerateKey(Shift entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Shift ParseEntity(Shift entity)
        {
            try
            {
                if (entity.Doctor != null)
                    entity.Doctor = doctorRepository.GetByID(entity.Doctor.GetKey());
                if (entity.AssignedExamRoom != null)
                    entity.AssignedExamRoom = roomRepository.GetByID(entity.AssignedExamRoom.GetKey());
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }
    }
}