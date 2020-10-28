// File:    Hospitalization.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class Hospitalization

using Model.HospitalResources;
using Model.Miscellaneous;
using Model.Users.Patient;
using Model.Utilities;
using System;
using System.Collections.Generic;

namespace Model.Schedule.Hospitalizations
{
    public class Hospitalization : Repository.Generics.Entity<int>
    {
        private Diagnosis diagnosis;
        private String causeOfAdmission;
        private DischargeType dischargeType;
        private Room room;
        private List<EquipmentUnit> equipmentInUse;
        private HospitalizationType hospitalizationType;
        private TimeInterval timeInterval;
        private Patient patient;
        private int id;

        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string CauseOfAdmission { get => causeOfAdmission; set => causeOfAdmission = value; }
        public DischargeType DischargeType { get => dischargeType; set => dischargeType = value; }
        public Room Room { get => room; set => room = value; }
        public HospitalizationType HospitalizationType { get => hospitalizationType; set => hospitalizationType = value; }
        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public int Id { get => id; set => id = value; }

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
                {
                    foreach (EquipmentUnit equipment in value)
                        AddEquipmentInUse(equipment);
                }
            }
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
            return obj is Hospitalization hospitalization &&
                   id == hospitalization.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}