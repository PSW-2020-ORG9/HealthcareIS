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

                // Med history
                .Include(p => p.MedicalRecord)
                .ThenInclude(mh => mh.Allergies)
                .ThenInclude(a => a.Allergy)

                .Include(p => p.MedicalRecord)
                .ThenInclude(mh => mh.FamilyMemberDiagnoses);
        }

        public bool ExistsByJMBG(string jmbg)
            => GetByJMBG(jmbg) != null;

        public Patient GetByJMBG(string jmbg)
            => GetMatching(p => p.Person.Jmbg == jmbg).FirstOrDefault();
    }
}
