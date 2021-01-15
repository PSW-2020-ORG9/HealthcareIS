using System.Collections.Generic;

namespace WPFHospitalEditor.Model
{
    public class Room : Entity<int>
    {
        public string Name { get; set; }
        public IEnumerable<EquipmentUnit> Equipment { get; set; }
    }
}