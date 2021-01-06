using System.Linq;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using User.API.Infrastructure.Repositories.Users.Patients.Interfaces;
using User.API.Model.Users.Patients;

namespace User.API.Infrastructure.Repositories.Users.Patients
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
                .Include(p => p.Allergies)
                .ThenInclude(a => a.Allergy);

        }

        public bool ExistsByJMBG(string jmbg)
            => GetByJMBG(jmbg) != null;

        public Patient GetByJMBG(string jmbg)
            => GetMatching(p => p.Person.Id == jmbg).FirstOrDefault();
    }
}
