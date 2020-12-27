using General;

namespace Hospital.API.Model.Medication
{
    public class SideEffect : Entity<int>
    {
        public string Name { get; set; }
        public int MedicationId { get; set; }
    }
}