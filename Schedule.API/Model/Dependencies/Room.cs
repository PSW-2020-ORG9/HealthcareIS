namespace Schedule.API.Model.Dependencies
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}