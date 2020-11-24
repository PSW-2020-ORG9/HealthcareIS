using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model.Schedule.Procedures;
using Repository.Generics;

namespace HealthcareBase.Model.Filters
{
    public interface IFilter<T, ID> where T : Entity<ID> where ID : IComparable
    {
        public IEnumerable<Expression<Func<T, bool>>> GetFilterExpression();
    }
}