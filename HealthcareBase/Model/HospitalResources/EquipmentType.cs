// File:    EquipmentType.cs
// Author:  Lana
// Created: 18 April 2020 16:54:14
// Purpose: Definition of Class EquipmentType

using System;

namespace Model.HospitalResources
{
    public class EquipmentType : Repository.Generics.Entity<int>
    {
        private String name;
        private String purpose;
        private Boolean requiresRenovationToMove;
        private int id;

        public EquipmentType(string name, string purpose, bool requiresRenovationToMove)
        {
            this.name = name;
            this.purpose = purpose;
            this.requiresRenovationToMove = requiresRenovationToMove;
        }

        public EquipmentType()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Purpose { get => purpose; set => purpose = value; }
        public bool RequiresRenovationToMove { get => requiresRenovationToMove; set => requiresRenovationToMove = value; }

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
            return obj is EquipmentType type &&
                   id == type.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}