// File:    HospitalizationType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class HospitalizationType

using Model.HospitalResources;
using System;
using System.Collections.Generic;

namespace Model.Schedule.Hospitalizations
{
    public class HospitalizationType : Repository.Generics.Entity<int>
    {
        private String name;
        private int usualNumberOfDays;
        private List<Department> appropriateDepartments;
        protected List<EquipmentType> necessaryEquipment;
        private int id;

        public IEnumerable<Department> AppropriateDepartments
        {
            get
            {
                if (appropriateDepartments == null)
                    appropriateDepartments = new List<Department>();
                return appropriateDepartments;
            }
            set
            {
                RemoveAllAppropriateDepartments();
                if (value != null)
                {
                    foreach (Model.HospitalResources.Department oDepartment in value)
                        AddAppropriateDepartments(oDepartment);
                }
            }
        }

        public void AddAppropriateDepartments(Model.HospitalResources.Department newDepartment)
        {
            if (newDepartment == null)
                return;
            if (this.appropriateDepartments == null)
                this.appropriateDepartments = new List<Department>();
            if (!this.appropriateDepartments.Contains(newDepartment))
                this.appropriateDepartments.Add(newDepartment);
        }

        public void RemoveAppropriateDepartments(Model.HospitalResources.Department oldDepartment)
        {
            if (oldDepartment == null)
                return;
            if (this.appropriateDepartments != null)
                if (this.appropriateDepartments.Contains(oldDepartment))
                    this.appropriateDepartments.Remove(oldDepartment);
        }

        public void RemoveAllAppropriateDepartments()
        {
            if (appropriateDepartments != null)
                appropriateDepartments.Clear();
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

        public int Id { get => id; set => id = value; }
        public int UsualNumberOfDays { get => usualNumberOfDays; set => usualNumberOfDays = value; }
        public string Name { get => name; set => name = value; }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is HospitalizationType type &&
                   id == type.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}