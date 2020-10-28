// File:    Ingredient.cs
// Author:  Lana
// Created: 14 April 2020 20:43:24
// Purpose: Definition of Class Ingredient

namespace Model.Medication
{
    public class Ingredient
    {
        public Ingredient(string name, bool isAllergen)
        {
            Name = name;
            IsAllergen = isAllergen;
        }

        public Ingredient()
        {
        }

        public string Name { get; set; }

        public bool IsAllergen { get; set; }
    }
}