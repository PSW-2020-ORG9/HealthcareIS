using General;
using Hospital.API.Model.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.API.Model.Resources
{
    public class Renovation : Entity<int>
    {
        public Renovation()
        {
            Room = new Room();
            TimeInterval = new TimeInterval();
        }
     
        public string Description { get; set; }
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}