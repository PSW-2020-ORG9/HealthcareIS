using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public class ExaminationSqlRepository : GenericSqlRepository<Examination, int>, ExaminationRepository
    {
        public ExaminationSqlRepository(IContextFactory factory) : base(factory) {}
        
        protected override IQueryable<Examination> IncludeFields(IQueryable<Examination> query)
        {
            return query
                .Include(examination => examination.ExaminationReport)
                .ThenInclude(report => report.Diagnoses)

                .Include(examination => examination.ExaminationReport)
                .ThenInclude(report => report.Prescriptions)
                .ThenInclude(prescription => prescription.Instructions)
                
                .Include(examination => examination.ExaminationReport)
                .ThenInclude(report => report.Prescriptions)
                .ThenInclude(prescription => prescription.Medication)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Person)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Department)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Specialties)

               .Include(examination => examination.Patient)
               .ThenInclude(patient => patient.Person); 
        }

        public IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetByRoomAndTime(Room room, TimeInterval time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetByPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}