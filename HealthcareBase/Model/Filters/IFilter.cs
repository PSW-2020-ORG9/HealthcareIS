using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Filters
{
    public interface IFilter<T, ID> where T : IEntity<ID> where ID : IComparable
    {
        public IEnumerable<Expression<Func<T, bool>>> GetFilterExpression();
    }
}