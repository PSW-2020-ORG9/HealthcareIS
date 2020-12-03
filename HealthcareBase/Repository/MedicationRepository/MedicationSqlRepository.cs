﻿using HealthcareBase.Model.Database;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;
using System;
using System.Linq;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationSqlRepository : GenericSqlRepository<Medication, int>, IMedicationRepository
    {
        public MedicationSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override IQueryable<Medication> IncludeFields(IQueryable<Medication> query)
        {
            throw new NotImplementedException();
        }
    }
}
