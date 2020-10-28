// File:    Hospitalization.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class Hospitalization

using System.Collections.Generic;
using Model.HospitalResources;
using Model.Miscellaneous;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;

namespace Model.Schedule.Hospitalizations
{
    public class Hospitalization : Entity<int>
    {
        private List<EquipmentUnit> equipmentInUse;

        public Diagnosis Diagnosis { get; set; }

        public string CauseOfAdmission { get; set; }

        public DischargeType DischargeType { get; set; }

        public Room Room { get; set; }

        public HospitalizationType HospitalizationType { get; set; }

        public TimeInterval TimeInterval { get; set; }

        public Patient Patient { get; set; }

        public int Id { get; set; }

        public IEnumerable<EquipmentUnit> EquipmentInUse
        {
            get
            {
                if (equipmentInUse == null)
                    equipmentInUse = new List<EquipmentUnit>();
                return equipmentInUse;
            }
            set
            {
                RemoveAllEquipmentInUse();
                if (value != null)
                    foreach (var equipment in value)
                        AddEquipmentInUse(equipment);
            }
        }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public void AddEquipmentInUse(EquipmentUnit newEquipment)
        {
            if (newEquipment == null)
                return;
            if (equipmentInUse == null)
                equipmentInUse = new List<EquipmentUnit>();
            if (!equipmentInUse.Contains(newEquipment))
                equipmentInUse.Add(newEquipment);
        }

        public void RemoveEquipmentInUse(EquipmentUnit oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (equipmentInUse != null)
                if (equipmentInUse.Contains(oldEquipment))
                    equipmentInUse.Remove(oldEquipment);
        }

        public void RemoveAllEquipmentInUse()
        {
            if (equipmentInUse != null)
                equipmentInUse.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is Hospitalization hospitalization &&
                   Id == hospitalization.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}