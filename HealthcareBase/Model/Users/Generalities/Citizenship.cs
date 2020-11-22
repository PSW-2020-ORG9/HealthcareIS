using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Model.Users.Generalities
{
    public class Citizenship
    {
        public string PersonJmbg { get; set; }
        
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}