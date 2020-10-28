// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using Model.HospitalResources;
using Model.Users.Employee;
using System;
using System.Collections.Generic;

namespace Model.Schedule.Procedures
{
    public class ProcedureType : Repository.Generics.Entity<int>
    {
        protected String name;
        protected TimeSpan duration;
        protected ProcedureKind kind;
        protected List<EquipmentType> necessaryEquipment;
        protected List<Specialty> qualifiedSpecialties;
        protected ProcedurePriority priority;
        private Boolean schedulableByPatient;
        protected int id;

        public string Name { get => name; set => name = value; }
        public TimeSpan Duration { get => duration; set => duration = value; }
        public ProcedureKind Kind { get => kind; set => kind = value; }
        public ProcedurePriority Priority { get => priority; set => priority = value; }
        public bool SchedulableByPatient { get => schedulableByPatient; set => schedulableByPatient = value; }
        public int Id { get => id; set => id = value; }

        public IEnumerable<Specialty> QualifiedSpecialties
        {
            get
            {
                if (qualifiedSpecialties == null)
                    qualifiedSpecialties = new List<Specialty>();
                return qualifiedSpecialties;
            }
            set
            {
                RemoveAllQualifiedSpecialties();
                if (value != null)
                {
                    foreach (Model.Users.Employee.Specialty oSpecialty in value)
                        AddQualifiedSpecialty(oSpecialty);
                }
            }
        }

        public void AddQualifiedSpecialty(Model.Users.Employee.Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (this.qualifiedSpecialties == null)
                this.qualifiedSpecialties = new List<Specialty>();
            if (!this.qualifiedSpecialties.Contains(newSpecialty))
                this.qualifiedSpecialties.Add(newSpecialty);
        }

        public void RemoveQualifiedSpecialty(Model.Users.Employee.Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (this.qualifiedSpecialties != null)
                if (this.qualifiedSpecialties.Contains(oldSpecialty))
                    this.qualifiedSpecialties.Remove(oldSpecialty);
        }

        public void RemoveAllQualifiedSpecialties()
        {
            if (qualifiedSpecialties != null)
                qualifiedSpecialties.Clear();
        }

        public IEnumerable<EquipmentType> NecessaryEquipment
        {
            get
            {
                if (necessaryEquipment == null)
                    necessaryEquipment = new List<EquipmentType>();
                return necessaryEquipment;
            }
            set
            {
                RemoveAllNecessaryEquipment();
                if (value != null)
                {
                    foreach (Model.HospitalResources.EquipmentType oEquipmentType in value)
                        AddNecessaryEquipment(oEquipmentType);
                }
            }
        }


        public void AddNecessaryEquipment(Model.HospitalResources.EquipmentType newEquipmentType)
        {
            if (newEquipmentType == null)
                return;
            if (this.necessaryEquipment == null)
                this.necessaryEquipment = new List<EquipmentType>();
            if (!this.necessaryEquipment.Contains(newEquipmentType))
                this.necessaryEquipment.Add(newEquipmentType);
        }

        public void RemoveNecessaryEquipment(Model.HospitalResources.EquipmentType oldEquipmentType)
        {
            if (oldEquipmentType == null)
                return;
            if (this.necessaryEquipment != null)
                if (this.necessaryEquipment.Contains(oldEquipmentType))
                    this.necessaryEquipment.Remove(oldEquipmentType);
        }

        public void RemoveAllNecessaryEquipment()
        {
            if (necessaryEquipment != null)
                necessaryEquipment.Clear();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

    }
}