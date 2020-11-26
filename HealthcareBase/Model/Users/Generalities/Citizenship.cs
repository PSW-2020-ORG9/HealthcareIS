using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareBase.Model.Users.Generalities
{
    public class Citizenship
    {
        public string PersonJmbg { get; set; }
        
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}