// File:    Room.cs
// Author:  Lana
// Created: 18 April 2020 17:07:59
// Purpose: Definition of Class Room

using System;
using System.Collections.Generic;

namespace Model.HospitalResources
{
    public class Room : Repository.Generics.Entity<int>
    {
        private String name;
        private RoomType purpose;
        private List<EquipmentUnit> equipment;
        private Department department;
        private int id;

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

        public string Name { get => name; set => name = value; }
        public RoomType Purpose { get => purpose; set => purpose = value; }
        public Department Department { get => department; set => department = value; }

        public int Id { get => id; set => id = value; }

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
                {
                    foreach (EquipmentUnit equipment in value)
                        AddEquipment(equipment);
                }
            }
        }


        public void AddEquipment(EquipmentUnit newEquipment)
        {
            if (newEquipment == null)
                return;
            if (equipment == null)
                equipment = new List<EquipmentUnit>();
            if (!equipment.Contains(newEquipment))
            {
                equipment.Add(newEquipment);
            }
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
            return obj is Room room &&
                   id == room.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}