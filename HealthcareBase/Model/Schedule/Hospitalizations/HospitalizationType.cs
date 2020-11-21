// File:    HospitalizationType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class HospitalizationType

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Hospitalizations
{
    public class HospitalizationType : Entity<int>
    {
        private List<Department> appropriateDepartments;
        protected List<EquipmentType> necessaryEquipment;

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
                    foreach (var oDepartment in value)
                        AddAppropriateDepartments(oDepartment);
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

        [Key]
        public int Id { get; set; }

        public int UsualNumberOfDays { get; set; }

        public string Name { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public void AddAppropriateDepartments(Department newDepartment)
        {
            if (newDepartment == null)
                return;
            if (appropriateDepartments == null)
                appropriateDepartments = new List<Department>();
            if (!appropriateDepartments.Contains(newDepartment))
                appropriateDepartments.Add(newDepartment);
        }

        public void RemoveAppropriateDepartments(Department oldDepartment)
        {
            if (oldDepartment == null)
                return;
            if (appropriateDepartments != null)
                if (appropriateDepartments.Contains(oldDepartment))
                    appropriateDepartments.Remove(oldDepartment);
        }

        public void RemoveAllAppropriateDepartments()
        {
            if (appropriateDepartments != null)
                appropriateDepartments.Clear();
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

        public override bool Equals(object obj)
        {
            return obj is HospitalizationType type &&
                   Id == type.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}