using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Users.Patient;
using Model.Schedule.Procedures;
using Model.Schedule.Hospitalizations;

namespace Service.UsersService.PatientService
{
    public class PatientChartDTO
    {
        private Patient patient;
        private IEnumerable<Examination> examinations;
        private IEnumerable<Surgery> surgeries;
        private IEnumerable<Hospitalization> hospitalizations;

        public PatientChartDTO() { }

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }


        public IEnumerable<Examination> Examinations
        {
            get { return examinations; }
            set { examinations = value; }
        }

        public IEnumerable<Surgery> Surgeries
        {
            get { return surgeries; }
            set { surgeries = value; }
        }


        public IEnumerable<Hospitalization> Hospitalizations
        {
            get { return hospitalizations; }
            set { hospitalizations = value; }
        }




    }
}
