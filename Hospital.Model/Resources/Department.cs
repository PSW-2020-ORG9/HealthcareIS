using General;

namespace Hospital.API.Model.Resources
{
    public class Department : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}