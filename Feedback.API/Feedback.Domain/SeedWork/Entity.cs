
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Feedback.Domain.SeedWork
{
    public abstract class Entity<ID> where ID : IComparable
    {
        public ID Id { get; private set; }
    }
}
