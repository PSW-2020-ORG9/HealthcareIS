// File:    Room.cs
// Author:  Lana
// Created: 18 April 2020 17:07:59
// Purpose: Definition of Class Room

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;


namespace User.API.Model.HospitalResources
{
    public class Room : Entity<int>
    {
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public RoomType Purpose { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public IEnumerable<EquipmentUnit> Equipment { get; set; }
    }
}