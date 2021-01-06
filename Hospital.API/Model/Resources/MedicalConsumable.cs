using General;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.API.Model.Resources
{
    public class MedicalConsumable : Entity<int>
    {
        public MedicalConsumable()
        {
            ConsumableType = new MedicalConsumableType();
        }

        public string Manufacturer { get; set; }
        public string Description { get; set; }

        [ForeignKey("ConsumableType")]
        public int? ConsumableTypeId { get; set; }
        public MedicalConsumableType ConsumableType { get; set; }
    }
}