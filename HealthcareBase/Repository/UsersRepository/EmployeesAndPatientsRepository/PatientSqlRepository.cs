using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using Model.Users.Patient;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System.Linq;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class PatientSqlRepository : GenericSqlRepository<Patient, int>, PatientRepository
    {
        public PatientSqlRepository(IContextFactory factory): base(factory) { }

        public override IQueryable<Patient> IncludeFields(IQueryable<Patient> query)
        {
            return query
                .Include(p => p.CityOfBirth)
                    .ThenInclude(c => c.Country)
                .Include(p => p.CityOfResidence)
                    .ThenInclude(c => c.Country)
                .Include(p => p.MedicalHistory)
                    .ThenInclude(mh => mh.Allergies)
                        .ThenInclude(a => a.Allergy)
                .Include(p => p.MedicalHistory)
                    .ThenInclude(mh => mh.PersonalHistory)
                .Include(p => p.MedicalHistory)
                    .ThenInclude(mh => mh.FamilyHistory);
        }

        public bool ExistsByJMBG(string jmbg)
        {
            return GetByJMBG(jmbg) != null;
        }

        public Patient GetByJMBG(string jmbg)
        {
            return GetMatching(p => p.Jmbg.Equals(jmbg)).FirstOrDefault();
        }
    }
}
