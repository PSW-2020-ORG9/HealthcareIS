﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using General;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Filters
{
    public abstract class AbstractFilter<T, ID> 
        : IFilter<T, ID>
        where T : Entity<ID> where ID : IComparable
    {
        private readonly List<Expression<Func<T, bool>>> _expressionFunctions = new List<Expression<Func<T, bool>>>();

        public IEnumerable<Expression<Func<T, bool>>> GetFilterExpression()
        {
            ConfigureFilter();
            return _expressionFunctions;
        }
        protected virtual void ConfigureFilter() {}

        protected void AddExpressionFunction(Expression<Func<T, bool>> expressionFunc)
            => _expressionFunctions.Add(expressionFunc);
    }
}