// File:    MedicalConsumable.cs
// Author:  Lana
// Created: 18 April 2020 17:49:09
// Purpose: Definition of Class MedicalConsumable

using System;

namespace Model.HospitalResources
{
    public class MedicalConsumable : Repository.Generics.Entity<int>
    {
        private String manufacutrer;
        private String description;
        private MedicalConsumableType consumableType;
        private int id;

        public MedicalConsumable(string manufacutrer, string description, MedicalConsumableType consumableType)
        {
            this.manufacutrer = manufacutrer;
            this.description = description;
            this.consumableType = consumableType;
        }

        public MedicalConsumable()
        {
            consumableType = new MedicalConsumableType();
        }

        public string Manufacutrer { get => manufacutrer; set => manufacutrer = value; }
        public string Description { get => description; set => description = value; }
        public MedicalConsumableType ConsumableType { get => consumableType; set => consumableType = value; }

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
            return obj is MedicalConsumable consumable &&
                   id == consumable.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}