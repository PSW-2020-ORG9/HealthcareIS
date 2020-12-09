using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.ScheduleRepository.ProceduresRepository
{
    public class ExaminationSqlRepository : GenericSqlRepository<Examination, int>, IExaminationRepository
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

                .Include(examination => examination.ExaminationReport)
                .ThenInclude(report => report.Prescriptions)
                .ThenInclude(prescription => prescription.Diagnosis)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Person)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Department)

                .Include(examination => examination.Doctor)
                .ThenInclude(doctor => doctor.Specialties)
                .ThenInclude(specialty => specialty.Specialty)

                .Include(examination => examination.Patient)
                .ThenInclude(patient => patient.Person)

                .Include(examination => examination.ProcedureDetails)
                .ThenInclude(details => details.RequiredSpecialty);

        }


        public IEnumerable<Examination> GetByDoctorAndDates(Doctor doctor, IEnumerable<DateTime> dates)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Examination> GetByPatientId(int patientId)
            => GetMatching(examination => examination.PatientId == patientId);
        public IEnumerable<Examination> GetByDoctorAndDate(int doctorId, DateTime date)
        {
            return GetMatching(e => e.Doctor.Id == doctorId
                                && e.TimeInterval.Start.Date.Equals(date));
        }

        public IEnumerable<Examination> GetByDoctorAndExaminationStart(int doctorId, DateTime date)
        {
            return GetMatching(e => e.Doctor.Id == doctorId
                                    && e.TimeInterval.Start.Equals(date));
        }
    }
}