// File:    Room.cs
// Author:  Lana
// Created: 18 April 2020 17:07:59
// Purpose: Definition of Class Room

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Generics;

namespace Model.HospitalResources
{
    public class Room : Entity<int>
    {
        private List<EquipmentUnit> equipment;

        public Room(string name, RoomType purpose, List<EquipmentUnit> equipment, Department department)
        {
            Name = name;
            Purpose = purpose;
            Equipment = equipment;
            Department = department;
        }

        public Room()
        {
        }

        public string Name { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public RoomType Purpose { get; set; }

        public Department Department { get; set; }

        [Key]
        public int Id { get; set; }

        public IEnumerable<EquipmentUnit> Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new List<EquipmentUnit>();
                return equipment;
            }
            set
            {
                RemoveAllEquipment();
                if (value != null)
                    foreach (var equipment in value)
                        AddEquipment(equipment);
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


        public void AddEquipment(EquipmentUnit newEquipment)
        {
            if (newEquipment == null)
                return;
            if (equipment == null)
                equipment = new List<EquipmentUnit>();
            if (!equipment.Contains(newEquipment)) equipment.Add(newEquipment);
        }

        public void RemoveEquipment(EquipmentUnit oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (equipment != null)
                if (equipment.Contains(oldEquipment))
                    equipment.Remove(oldEquipment);
        }

        public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is Room room &&
                   Id == room.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}