using System.ComponentModel.DataAnnotations.Schema;
using User.API.Model.Locale;

namespace User.API.Model.Generalities
{
    public class Citizenship
    {
        public string PersonJmbg { get; set; }
        
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}