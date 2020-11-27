﻿using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class EquipmentSqlRepository : GenericSqlRepository<EquipmentUnit, int>, IEquipmentUnitRepository
    {
        public EquipmentSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

        public IEnumerable<EquipmentUnit> GetByCurrentLocationWithoutParse(Room room)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<EquipmentUnit> IncludeFields(IQueryable<EquipmentUnit> query)
        {
            return query.
                Include(unit => unit.CurrentLocation)
                .ThenInclude(location => location.Department)

                .Include(unit => unit.EquipmentType);
        }
    }
}