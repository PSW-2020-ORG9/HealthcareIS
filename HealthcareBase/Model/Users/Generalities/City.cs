// File:    City.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class City

using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Generalities
{
    public class City : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;
    }
}