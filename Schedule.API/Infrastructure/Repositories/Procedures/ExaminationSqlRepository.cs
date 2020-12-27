using System;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Infrastructure.Repositories.Procedures
{
    public class ExaminationSqlRepository : GenericSqlRepository<Examination, int>, IExaminationRepository
    {
        public ExaminationSqlRepository(IContextFactory factory) : base(factory) {}
        
        protected override IQueryable<Examination> IncludeFields(IQueryable<Examination> query)
        {
            return query
                .Include(examination => examination.ExaminationReport)
                .Include(examination  => examination.ExaminationReport.Diagnoses);
        }
        
        public IEnumerable<Examination> GetByDoctorAndDates(Doctor doctor, IEnumerable<DateTime> dates)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Examination> GetByPatientId(int patientId)
            => GetMatching(examination => examination.PatientId == patientId);
        
        public IEnumerable<Examination> GetByDoctorAndDate(int doctorId, DateTime date)
        {
            return GetMatching(e => e.DoctorId == doctorId
                                && e.TimeInterval.Start.Date.Equals(date));
        }

        public IEnumerable<Examination> GetByDoctorAndExaminationStart(int doctorId, DateTime date)
        {
            return GetMatching(e => 
                e.DoctorId == doctorId
                && e.TimeInterval.Start.Equals(date));
        }
    }
}