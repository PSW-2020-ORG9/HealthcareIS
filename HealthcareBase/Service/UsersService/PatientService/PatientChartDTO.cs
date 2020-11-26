using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Patient;

namespace HealthcareBase.Service.UsersService.PatientService
{
    public class PatientChartDTO
    {
        public Patient Patient { get; set; }


        public IEnumerable<Examination> Examinations { get; set; }

        public IEnumerable<Surgery> Surgeries { get; set; }


        public IEnumerable<Hospitalization> Hospitalizations { get; set; }
    }
}