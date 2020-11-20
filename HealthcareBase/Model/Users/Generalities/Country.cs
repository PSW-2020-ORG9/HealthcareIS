// File:    Country.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Country

using Repository.Generics;
using System.ComponentModel.DataAnnotations;

namespace Model.Users.Generalities
{
    public class Country : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;

        public override bool Equals(object obj) 
            => obj is Country country && Id == country.Id;
    }
}