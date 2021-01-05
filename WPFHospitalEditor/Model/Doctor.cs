namespace WPFHospitalEditor.Model
{
    public class Doctor
    {
        public int Id { get; internal set; }
        public int DepartmentId { get; internal set; }
        public Person Person { get; set; }
    }
}
