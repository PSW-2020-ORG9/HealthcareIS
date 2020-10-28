// File:    HospitalizationFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Class HospitalizationFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.MiscellaneousRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Repository.ScheduleRepository.HospitalizationsRepository
{
    public class HospitalizationFileRepository : GenericFileRepository<Hospitalization, int>, HospitalizationRepository
    {
        private HospitalizationTypeRepository hospitalizationTypeRepository;
        private RoomRepository roomRepository;
        private EquipmentUnitRepository equipmentUnitRepository;
        private PatientRepository patientRepository;
        private DiagnosisRepository diagnosisRepository;
        private IntegerKeyGenerator keyGenerator;
        
        public HospitalizationFileRepository(HospitalizationTypeRepository hospitalizationTypeRepository, 
            RoomRepository roomRepository, EquipmentUnitRepository equipmentUnitRepository, 
            PatientRepository patientRepository, DiagnosisRepository diagnosisRepository, String filePath) : base(filePath)
        {
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
            this.roomRepository = roomRepository;
            this.equipmentUnitRepository = equipmentUnitRepository;
            this.patientRepository = patientRepository;
            this.diagnosisRepository = diagnosisRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Hospitalization> GetByEquipmentInUseAndTime(IEnumerable<EquipmentUnit> equipment, TimeInterval time)
        {
            Predicate<Hospitalization> timeOverlaps =
                hospitalization => hospitalization.TimeInterval.Overlaps(time);
            Predicate<Hospitalization> equipmentInUseOverlaps =
                hospitalization => hospitalization.EquipmentInUse.Intersect(equipment).Count() > 0;
            return GetMatching(hospitalization => timeOverlaps(hospitalization) && equipmentInUseOverlaps(hospitalization));
        }

        public IEnumerable<Hospitalization> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            return GetMatching(hospitalization => hospitalization.Patient.Equals(patient) && hospitalization.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Hospitalization> GetByRoomAndTime(Room room, TimeInterval time)
        {
            return GetMatching(hospitalization => room.Equals(hospitalization.Room) && hospitalization.TimeInterval.Overlaps(time));
        }
        public IEnumerable<Hospitalization> GetByPatient(Patient patient)
        {
            List<Hospitalization> hospitalizations = new List<Hospitalization>();
            IEnumerable<Hospitalization> retHospitalizations;

            foreach (Hospitalization currentHospitalization in GetAll())
            {
                if (currentHospitalization.Patient.Equals(patient))
                    hospitalizations.Add(currentHospitalization);
            }
            retHospitalizations = hospitalizations;

            return retHospitalizations;

        }
        
        protected override Hospitalization ParseEntity(Hospitalization entity)
        {
            try
            {
                if (entity.HospitalizationType != null)
                    entity.HospitalizationType = hospitalizationTypeRepository.GetByID(entity.HospitalizationType.GetKey());
                if (entity.Patient != null)
                    entity.Patient = patientRepository.GetByID(entity.Patient.GetKey());
                if (entity.Room != null)
                    entity.Room = roomRepository.GetByID(entity.GetKey());
                List<EquipmentUnit> equipmentInUse = new List<EquipmentUnit>();
                foreach (EquipmentUnit equipment in entity.EquipmentInUse)
                    equipmentInUse.Add(equipmentUnitRepository.GetByID(equipment.GetKey()));
                entity.EquipmentInUse = equipmentInUse;
                if (entity.Diagnosis != null)
                    entity.Diagnosis = diagnosisRepository.GetByID(entity.Diagnosis.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(Hospitalization entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}