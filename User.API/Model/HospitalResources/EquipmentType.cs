// File:    EquipmentType.cs
// Author:  Lana
// Created: 18 April 2020 16:54:14
// Purpose: Definition of Class EquipmentType

using User.API.Infrastructure;

namespace User.API.Model.HospitalResources
{
    public class EquipmentType : Entity<int>
    {       
        public string Name { get; set; }
        public string Purpose { get; set; }
        public bool RequiresRenovationToMove { get; set; }
    }
}