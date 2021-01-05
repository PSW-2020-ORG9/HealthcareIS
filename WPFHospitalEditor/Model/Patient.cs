namespace WPFHospitalEditor.Model
{
    public class Patient
    {
        public Person Person { get; internal set; }
        public int Id { get; internal set; }
    }

    public class Person
    {
        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public string Id { get; internal set; }
    }
}
