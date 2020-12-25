using System;
using System.ComponentModel.DataAnnotations;

namespace Feedback.API.Infrastructure
{
    public abstract class Entity<ID> where ID : IComparable
    {
        [Key] public ID Id { get; set; }
    }
}