using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using General;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Services.Procedures
{
    public class ExaminationServiceProxy : Interface.IExaminationService
    {
        private readonly IExaminationService _examinationService;
        private readonly IConnection _roomConnection, _doctorConnection, _patientConnection;

        public ExaminationServiceProxy(
            IExaminationService examinationService, 
            IConnection roomConnection, IConnection doctorConnection, IConnection patientConnection
        )
        {
            this._examinationService = examinationService;
            this._roomConnection = roomConnection;
            this._doctorConnection = doctorConnection;
            this._patientConnection = patientConnection;
        }
        
        public IEnumerable<Examination> SimpleSearch(ExaminationSimpleFilterDto filterDto)
        {
            IEnumerable<Examination> examinations = _examinationService.SimpleSearch(filterDto).ToList();
            AttachMissingReferences(examinations);
            return examinations;
        }

        public IEnumerable<Examination> AdvancedSearch(ExaminationAdvancedFilterDto filterDto)
        {
            IEnumerable<Examination> examinations = _examinationService.AdvancedSearch(filterDto).ToList();
            AttachMissingReferences(examinations);
            return examinations;
        }

        public IEnumerable<Examination> GetByPatientId(int patientId)
        {
            IEnumerable<Examination> examinations = _examinationService.GetByPatientId(patientId).ToList();
            AttachMissingReferences(examinations);
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
            Examination examination = _examinationService.Schedule(procedure);
            
            if (!DoctorExists(procedure.DoctorId)) return null;
            if (!PatientExists(procedure.PatientId)) return null;
            
            AttachMissingReferences(new [] {examination});
            return examination;
        }
        
        private bool PatientExists(in int patientId)
        {
            try
            {
                IEnumerable<Patient> patients = _patientConnection.Post<IEnumerable<Patient>>(new[] {patientId});
                return patients.Count() == 1;
            }
            catch (SerializationException)
            {
                return false;
            }
        }
        
        private bool DoctorExists(in int doctorId)
        {
            try
            {
                IEnumerable<Doctor> doctors = _patientConnection.Post<IEnumerable<Doctor>>(new[] {doctorId});
                return doctors.Count() == 1;
            }
            catch (SerializationException)
            {
                return false;
            }
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
    }
}