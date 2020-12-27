using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Filters
{
    public interface IFilter<T, ID> where T : Entity<ID> where ID : IComparable
    {
        public IEnumerable<Expression<Func<T, bool>>> GetFilterExpression();
    }
}