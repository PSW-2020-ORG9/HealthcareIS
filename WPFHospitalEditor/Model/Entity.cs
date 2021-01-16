using System;
using System.ComponentModel.DataAnnotations;

namespace WPFHospitalEditor.Model
{
    public abstract class Entity<ID> where ID : IComparable
    {
        [Key] public ID Id { get; set; }
    }
}