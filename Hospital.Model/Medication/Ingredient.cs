using General;

namespace Hospital.API.Model.Medication
{
    public class Ingredient : Entity<int>
    {
        public string Name { get; set; }
        public bool IsAllergen { get; set; }
        public int MedicationId { get; set; }
    }
}