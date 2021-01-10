using General;

namespace User.API.Model.Promotions
{
    public class Advertisement : Entity<int>
    {
        public string PictureUrl { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}