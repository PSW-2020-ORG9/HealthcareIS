using General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.API.Model.Medication
{
    public class Medication : Entity<int>
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public MedicineType Type { get; set; }

        public IEnumerable<SideEffect> SideEffects { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}