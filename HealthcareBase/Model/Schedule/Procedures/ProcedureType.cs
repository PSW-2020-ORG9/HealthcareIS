// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class ProcedureType : Entity<int>
    {
        protected TimeSpan duration;
        protected int id;
        protected ProcedureKind kind;
        protected string name;
        protected List<EquipmentType> necessaryEquipment;
        protected ProcedurePriority priority;
        protected List<Specialty> qualifiedSpecialties;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public TimeSpan Duration
        {
            get => duration;
            set => duration = value;
        }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedureKind Kind
        {
            get => kind;
            set => kind = value;
        }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedurePriority Priority
        {
            get => priority;
            set => priority = value;
        }

        public bool SchedulableByPatient { get; set; }

        [Key]
        public int Id
        {
            get => id;
            set => id = value;
        }

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
                    foreach (var oSpecialty in value)
                        AddQualifiedSpecialty(oSpecialty);
            }
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
                    foreach (var oEquipmentType in value)
                        AddNecessaryEquipment(oEquipmentType);
            }
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public void AddQualifiedSpecialty(Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (qualifiedSpecialties == null)
                qualifiedSpecialties = new List<Specialty>();
            if (!qualifiedSpecialties.Contains(newSpecialty))
                qualifiedSpecialties.Add(newSpecialty);
        }

        public void RemoveQualifiedSpecialty(Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (qualifiedSpecialties != null)
                if (qualifiedSpecialties.Contains(oldSpecialty))
                    qualifiedSpecialties.Remove(oldSpecialty);
        }

        public void RemoveAllQualifiedSpecialties()
        {
            if (qualifiedSpecialties != null)
                qualifiedSpecialties.Clear();
        }


        public void AddNecessaryEquipment(EquipmentType newEquipmentType)
        {
            if (newEquipmentType == null)
                return;
            if (necessaryEquipment == null)
                necessaryEquipment = new List<EquipmentType>();
            if (!necessaryEquipment.Contains(newEquipmentType))
                necessaryEquipment.Add(newEquipmentType);
        }

        public void RemoveNecessaryEquipment(EquipmentType oldEquipmentType)
        {
            if (oldEquipmentType == null)
                return;
            if (necessaryEquipment != null)
                if (necessaryEquipment.Contains(oldEquipmentType))
                    necessaryEquipment.Remove(oldEquipmentType);
        }

        public void RemoveAllNecessaryEquipment()
        {
            if (necessaryEquipment != null)
                necessaryEquipment.Clear();
        }
    }
}