// File:    Country.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Country

using Repository.Generics;

namespace Model.Users.Generalities
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Country country &&
                   Id == country.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}