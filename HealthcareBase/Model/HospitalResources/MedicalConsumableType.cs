// File:    MedicalConsumableType.cs
// Author:  Lana
// Created: 18 April 2020 17:38:58
// Purpose: Definition of Class MedicalConsumableType

using System;

namespace Model.HospitalResources
{
    public class MedicalConsumableType : Repository.Generics.Entity<int>
    {
        private String name;
        private String purpose;
        private int id;

        public MedicalConsumableType()
        {
        }

        public MedicalConsumableType(string name, string purpose)
        {
            this.name = name;
            this.purpose = purpose;
        }

        public string Name { get => name; set => name = value; }
        public string Purpose { get => purpose; set => purpose = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is MedicalConsumableType type &&
                   id == type.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
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