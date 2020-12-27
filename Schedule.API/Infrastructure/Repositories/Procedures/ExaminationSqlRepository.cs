using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Infrastructure.Repositories.Procedures.Interface;
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
                .Include(examination => examination.ProcedureDetails)
                    
                .Include(examination => examination.ExaminationReport)
                .ThenInclude(report => report.Diagnoses);


            // TODO Crashes with included diagnoses.
            // Error msg: Unknown column e0.id in the where clause 
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