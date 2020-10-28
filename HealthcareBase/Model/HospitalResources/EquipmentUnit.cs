// File:    EquipmentUnit.cs
// Author:  Lana
// Created: 18 April 2020 17:00:15
// Purpose: Definition of Class EquipmentUnit

using System;

namespace Model.HospitalResources
{
    public class EquipmentUnit : Repository.Generics.Entity<int>
    {
        private DateTime acquisitionDate;
        private String manufacturer;
        private Room currentLocation;
        private EquipmentType equipmentType;
        private int id;

        public EquipmentUnit(DateTime acquisitionDate, string manufacturer, Room currentLocation, EquipmentType equipmentType)
        {
            this.acquisitionDate = acquisitionDate;
            this.manufacturer = manufacturer;
            this.currentLocation = currentLocation;
            this.equipmentType = equipmentType;
        }

        public EquipmentUnit()
        {
            equipmentType = new EquipmentType();
        }

        public DateTime AcquisitionDate { get => acquisitionDate; set => acquisitionDate = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public Room CurrentLocation { get => currentLocation; set { currentLocation = value; } }
        public EquipmentType EquipmentType { get => equipmentType; set => equipmentType = value; }

        public int Id { get => id; set => id = value; }

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
            return obj is EquipmentUnit unit &&
                   id == unit.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}