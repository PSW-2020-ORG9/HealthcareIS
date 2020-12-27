
using General;

namespace Schedule.API.Model.Procedures
{
    public class Diagnosis : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInjury { get; set; }
    }
}