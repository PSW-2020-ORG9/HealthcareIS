using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Model.Users.Generalities
{
    public class Citizenship : Entity<int>
    {
        [Key] 
        public int Id { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}