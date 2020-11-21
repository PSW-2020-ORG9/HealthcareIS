// File:    Entity.cs
// Author:  Lana
// Created: 27 May 2020 21:11:30
// Purpose: Definition of Interface Entity

using System;

namespace HealthcareBase.Repository.Generics
{
    public interface Entity<ID> where ID : IComparable
    {
        ID GetKey();
        void SetKey(ID id);
    }
}