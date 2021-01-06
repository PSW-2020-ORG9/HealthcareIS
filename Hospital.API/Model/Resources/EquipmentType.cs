using General;

namespace Hospital.API.Model.Resources
{
    public class EquipmentType : Entity<int>
    {       
        public string Name { get; set; }
        public string Purpose { get; set; }
        public bool RequiresRenovationToMove { get; set; }
    }
}