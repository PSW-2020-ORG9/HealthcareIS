// File:    Frequency.cs
// Author:  Lana
// Created: 14 April 2020 20:52:43
// Purpose: Definition of Class Frequency

using System;

namespace Model.Medication
{
    public class Frequency
    {
        private SideEffectFrequency value;
        private SideEffect sideEffect;

        public Frequency(SideEffectFrequency value, SideEffect sideEffect)
        {
            this.value = value;
            this.sideEffect = sideEffect;
        }

        public Frequency()
        {
            sideEffect = new SideEffect();
        }

        public SideEffectFrequency Value { get => value; set => this.value = value; }
        public SideEffect SideEffects { get => sideEffect; set => sideEffect = value; }
    }
}