// File:    SideEffect.cs
// Author:  Lana
// Created: 14 April 2020 20:46:07
// Purpose: Definition of Class SideEffect

using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Medication
{
    [Owned]
    public class SideEffect
    {
        public SideEffect(string name)
        {
            Name = name;
        }

        public SideEffect()
        {
        }

        public string Name { get; set; }
    }
}