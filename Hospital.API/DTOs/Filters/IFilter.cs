using General;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hospital.API.DTOs.Filters
{
    public interface IFilter<T, ID> where T : Entity<ID> where ID : IComparable
    {
        public IEnumerable<Expression<Func<T, bool>>> GetFilterExpression();
    }
}