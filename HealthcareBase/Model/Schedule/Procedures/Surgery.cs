// File:    Surgery.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Surgery

using Model.Miscellaneous;
using System;

namespace Model.Schedule.Procedures
{
    public class Surgery : Procedure
    {
        private Diagnosis diagnosis;
        private String causeOfSurgery;

        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string CauseOfSurgery { get => causeOfSurgery; set => causeOfSurgery = value; }

        public override bool Equals(object obj)
        {
            return obj is Surgery procedure &&
                   id == procedure.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}