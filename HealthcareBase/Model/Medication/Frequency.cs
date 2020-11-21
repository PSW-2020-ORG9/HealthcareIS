// File:    Frequency.cs
// Author:  Lana
// Created: 14 April 2020 20:52:43
// Purpose: Definition of Class Frequency

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Medication
{
    [Owned]
    public class Frequency
    {
        public Frequency(SideEffectFrequency value, SideEffect sideEffect)
        {
            Value = value;
            SideEffects = sideEffect;
        }

        public Frequency()
        {
            SideEffects = new SideEffect();
        }

        [Column(TypeName = "nvarchar(24)")]
        public SideEffectFrequency Value { get; set; }

        public SideEffect SideEffects { get; set; }
    }
}