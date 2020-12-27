using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.API.Infrastructure.Database
{
    public abstract class Entity<ID> where ID : IComparable
    {
        [Key] public ID Id { get; set; }
    }
}