// File:    EquipmentType.cs
// Author:  Lana
// Created: 18 April 2020 16:54:14
// Purpose: Definition of Class EquipmentType

using Repository.Generics;

namespace Model.HospitalResources
{
    public class EquipmentType : Entity<int>
    {
        public EquipmentType(string name, string purpose, bool requiresRenovationToMove)
        {
            Name = name;
            Purpose = purpose;
            RequiresRenovationToMove = requiresRenovationToMove;
        }

        public EquipmentType()
        {
        }

        public string Name { get; set; }

        public string Purpose { get; set; }

        public bool RequiresRenovationToMove { get; set; }

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
            return obj is EquipmentType type &&
                   Id == type.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}