// File:    EquipmentUnit.cs
// Author:  Lana
// Created: 18 April 2020 17:00:15
// Purpose: Definition of Class EquipmentUnit

using System;
using Repository.Generics;

namespace Model.HospitalResources
{
    public class EquipmentUnit : Entity<int>
    {
        public EquipmentUnit(DateTime acquisitionDate, string manufacturer, Room currentLocation,
            EquipmentType equipmentType)
        {
            AcquisitionDate = acquisitionDate;
            Manufacturer = manufacturer;
            CurrentLocation = currentLocation;
            EquipmentType = equipmentType;
        }

        public EquipmentUnit()
        {
            EquipmentType = new EquipmentType();
        }

        public DateTime AcquisitionDate { get; set; }

        public string Manufacturer { get; set; }

        public Room CurrentLocation { get; set; }

        public EquipmentType EquipmentType { get; set; }

        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is EquipmentUnit unit &&
                   Id == unit.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}