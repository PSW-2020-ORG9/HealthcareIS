using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class PatientSqlRepository : GenericSqlRepository<Patient, int>, IPatientRepository
    {
        public PatientSqlRepository(IContextFactory factory): base(factory) { }

        protected override IQueryable<Patient> IncludeFields(IQueryable<Patient> query)
        {
            return query
                .Include(p => p.Person)
                    .ThenInclude(p => p.CityOfBirth)
                        .ThenInclude(c => c.Country)

                // City
                .Include(p => p.Person)
                    .ThenInclude(p => p.CityOfResidence)
                        .ThenInclude(c => c.Country)

                // Citizenship
                .Include(p => p.Person)
                    .ThenInclude(p => p.Citizenships)
                        .ThenInclude(cz => cz.Country)

                // Medical record
                .Include(p => p.MedicalRecord)
                    .ThenInclude(mr => mr.Allergies)
                        .ThenInclude(a => a.Allergy)

                .Include(p => p.MedicalRecord)
                    .ThenInclude(mr => mr.Examinations)
                        .ThenInclude(e => e.ExaminationReport)
                            .ThenInclude(er => er.Diagnoses)

                .Include(p => p.MedicalRecord)
                    .ThenInclude(mr => mr.Surgeries)

                .Include(p => p.MedicalRecord)
                    .ThenInclude(mh => mh.FamilyMemberDiagnoses);
        }

        public bool ExistsByJMBG(string jmbg)
            => GetByJMBG(jmbg) != null;

        public Patient GetByJMBG(string jmbg)
            => GetMatching(p => p.Person.Jmbg == jmbg).FirstOrDefault();

        public int GetMedicalRecordId(int patientId)
        {
            var foundPatient = GetByID(patientId);
            if (foundPatient == null) return -1;
            return foundPatient.MedicalRecordId;
        }
    }
}
