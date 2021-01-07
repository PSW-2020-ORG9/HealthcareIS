using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFHospitalEditor.Model
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