// File:    SideEffect.cs
// Author:  Lana
// Created: 14 April 2020 20:46:07
// Purpose: Definition of Class SideEffect

using System;

namespace Model.Medication
{
    public class SideEffect
    {
        private String name;

        public SideEffect(string name)
        {
            this.name = name;
        }

        public SideEffect()
        {
        }

        public string Name { get => name; set => name = value; }
    }
}