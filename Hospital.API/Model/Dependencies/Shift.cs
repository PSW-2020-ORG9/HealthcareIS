using System.ComponentModel.DataAnnotations.Schema;
using General;
using Hospital.API.Model.Resources;
using Hospital.API.Model.Utilities;

namespace Hospital.API.Model.Dependencies
{
    public class Shift : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("AssignedExamRoom")]
        public int AssignedExamRoomId { get; set; }
        public Room AssignedExamRoom { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}