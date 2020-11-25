using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthcareBase.Model.Filters;
using Model.Schedule.Procedures;
using Model.Users.Employee;

namespace Service.ScheduleService.ProcedureService
{
    public class DoctorCredentialsDto : AbstractFilter<Examination, int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        protected override void ConfigureFilter()
        {
            if (Name.Length > 0) 
                AddExpressionFunction(examination => examination.Doctor.Person.Name.Contains(Name));
            if (Surname.Length > 0)
                AddExpressionFunction(examination => examination.Doctor.Person.Surname.Contains(Surname));
        }
    }
}