using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using General;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures
{
    public class ExaminationServiceProxy : Interface.IExaminationService
    {
        private readonly ExaminationService _examinationService;
        private readonly IConnection _roomConnection, _doctorConnection, _patientConnection;

        public ExaminationServiceProxy(
            ExaminationService examinationService, 
            IConnection roomConnection, IConnection doctorConnection, IConnection patientConnection
        )
        {
            this._examinationService = examinationService;
            this._roomConnection = roomConnection;
            this._doctorConnection = doctorConnection;
            this._patientConnection = patientConnection;
        }

        public IEnumerable<Examination> Search(AbstractExaminationFilter filterDto)
        {
            filterDto.DoctorIds = FilterDoctorIdsByCredentials(filterDto);
            IEnumerable<Examination> examinations = _examinationService.Search(filterDto).ToList();
            AttachMissingReferences(examinations);
            return examinations;
        }

        private IEnumerable<int> FilterDoctorIdsByCredentials(AbstractExaminationFilter dto)
        {
             return GetAllDoctorIds()
                .Where(doctor =>
                    doctor.Person.Name.Contains(dto.DoctorName)
                    && doctor.Person.Surname.Contains(dto.DoctorSurname))
                .Select(doctor => doctor.Id);
        }

        private IEnumerable<Doctor> GetAllDoctorIds()
        {
            return _doctorConnection.Get<IEnumerable<Doctor>>();
        }

        public IEnumerable<Examination> GetByPatientId(int patientId)
        {
            IEnumerable<Examination> examinations = _examinationService.GetByPatientId(patientId).ToList();
            AttachMissingReferences(examinations);
            return examinations;
        }

        public IEnumerable<Examination> GetBySpecialtyId(int specialtyId)
        {
            IEnumerable<Examination> examinations = _examinationService.GetBySpecialtyId(specialtyId).ToList();
            return examinations;
        }

        public bool Cancel(int examinationId)
        {
            return _examinationService.Cancel(examinationId);
        }

        public Examination GetByID(int id)
        {
            Examination examination = _examinationService.GetByID(id);
            AttachMissingReferences(new [] {examination});
            return examination;
        }

        public Examination Schedule(Examination procedure)
        {
            AttachMissingReferences(new [] {procedure});
            return _examinationService.Schedule(procedure);
        }

        public Examination ScheduleEmergency(Examination procedure)
        {
            AttachMissingReferences(new[] { procedure });
            return _examinationService.ScheduleEmergency(procedure);
        }

        // Data reassembly methods
        private void AttachMissingReferences(IEnumerable<Examination> examinations)
        {
            List<Examination> examinationList = examinations.ToList();
            AttachDoctors(examinationList);
            AttachRooms(examinationList);
            AttachPatients(examinationList);
        }

        private void AttachDoctors(List<Examination> examinations)
        {
            HashSet<int> doctorIds = new HashSet<int>();
            
            foreach (var examination in examinations)
            {
                doctorIds.Add(examination.DoctorId);
            }

            IEnumerable<Doctor> doctors;
            try
            {
                doctors = _doctorConnection.Post<IEnumerable<Doctor>>(doctorIds.ToList()).ToList();
            }
            catch (SerializationException)
            {
                return;
            }
            
            foreach (var examination in examinations)
            {
                examination.Doctor = doctors.FirstOrDefault(doctor => doctor.Id == examination.DoctorId);
            }
        }

        private void AttachRooms(List<Examination> examinations)
        {
            HashSet<int> roomIds = new HashSet<int>();
            
            foreach (var examination in examinations)
            {
                roomIds.Add(examination.RoomId);
            }

            IEnumerable<Room> rooms;
            try
            {
                rooms = _roomConnection.Post<IEnumerable<Room>>(roomIds.ToList()).ToList();
            }
            catch (SerializationException)
            {
                return;
            }
            
            foreach (var examination in examinations)
            {
                examination.Room = rooms.FirstOrDefault(room => room.Id == examination.RoomId);
            }
        }

        private void AttachPatients(List<Examination> examinations)
        {
            HashSet<int> patientIds = new HashSet<int>();
            
            foreach (var examination in examinations)
            {
                patientIds.Add(examination.PatientId);
            }

            IEnumerable<Patient> patients;
            try
            {
                patients = _patientConnection.Post<IEnumerable<Patient>>(patientIds.ToList()).ToList();
            }
            catch (SerializationException)
            {
                return;
            }
            
            foreach (var examination in examinations)
            {
                examination.Patient = patients.FirstOrDefault(patient => patient.Id == examination.PatientId);
            }
        }

        public IEnumerable<Examination> GetByRoomId(int roomId)
            => _examinationService.GetByRoomId(roomId);
    }
}