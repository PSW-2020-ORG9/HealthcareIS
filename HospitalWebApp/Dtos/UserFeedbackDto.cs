namespace HospitalWebApp.Dtos
{
    public class UserFeedbackDto
    {
        public string UserComment { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublished { get; set; }
        public int UserId { get; set; }
    }
}