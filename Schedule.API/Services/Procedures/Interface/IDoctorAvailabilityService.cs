using System;
using System.Collections.Generic;
using Schedule.API.Model.Dependencies;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IDoctorAvailabilityService
    {
        IEnumerable<Doctor> GetAvailableByDay(DateTime date);
    }
}