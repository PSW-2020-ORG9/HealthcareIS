using System;
using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee.Doctors;

namespace HealthcareBase.Service.ScheduleService.ProcedureService.Interface
{
    public interface IDoctorAvailabilityService
    {
        public IEnumerable<Doctor> GetAvailableByDay(DateTime date);
    }
}