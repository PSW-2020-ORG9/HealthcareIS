using System.Collections.Generic;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Users.Patient;

namespace Service.UsersService.PatientService
{
    public class PatientChartDTO
    {
        public Patient Patient { get; set; }


        public IEnumerable<Examination> Examinations { get; set; }

        public IEnumerable<Surgery> Surgeries { get; set; }


        public IEnumerable<Hospitalization> Hospitalizations { get; set; }
    }
}