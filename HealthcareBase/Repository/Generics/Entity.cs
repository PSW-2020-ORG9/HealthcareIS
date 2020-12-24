// File:    Entity.cs
// Author:  Lana
// Created: 27 May 2020 21:11:30
// Purpose: Definition of Interface Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HealthcareBase.Repository.Generics
{
    public abstract class Entity<ID> where ID : IComparable
    {
        [Key] public ID Id { get; set; }
    }
}