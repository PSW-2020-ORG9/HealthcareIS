using General;

namespace Hospital.API.Model.Resources
{
    public class MedicalConsumableType : Entity<int>
    {
        public MedicalConsumableType()
        {
        }
        
        public string Name { get; set; }
        public string Purpose { get; set; }
    }
}