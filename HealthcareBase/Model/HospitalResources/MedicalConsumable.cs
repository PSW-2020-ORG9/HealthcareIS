// File:    MedicalConsumable.cs
// Author:  Lana
// Created: 18 April 2020 17:49:09
// Purpose: Definition of Class MedicalConsumable

using Repository.Generics;

namespace Model.HospitalResources
{
    public class MedicalConsumable : Entity<int>
    {
        public MedicalConsumable(string manufacutrer, string description, MedicalConsumableType consumableType)
        {
            Manufacutrer = manufacutrer;
            Description = description;
            ConsumableType = consumableType;
        }

        public MedicalConsumable()
        {
            ConsumableType = new MedicalConsumableType();
        }

        public string Manufacutrer { get; set; }

        public string Description { get; set; }

        public MedicalConsumableType ConsumableType { get; set; }

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
            return obj is MedicalConsumable consumable &&
                   Id == consumable.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}