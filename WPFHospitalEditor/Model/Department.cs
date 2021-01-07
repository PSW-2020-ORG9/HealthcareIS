namespace WPFHospitalEditor.Model
{
    public class Department : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}