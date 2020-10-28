// File:    ScheduleProcedure.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleProcedure

using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;
using System;

namespace Model.Requests
{
    public class ScheduleProcedure : ScheduleAdjustmentRequest
    {
        private Patient patient;
        private ProcedureType type;
        private ProcedureSchedulingPreference preference;

        public Patient Patient { get => patient; set => patient = value; }
        public ProcedureType Type { get => type; set => type = value; }
        public ProcedureSchedulingPreference Preference { get => preference; set => preference = value; }

        public override bool Equals(object obj)
        {
            return obj is ScheduleProcedure request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}