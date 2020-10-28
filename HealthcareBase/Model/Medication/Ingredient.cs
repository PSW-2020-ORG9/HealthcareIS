// File:    Ingredient.cs
// Author:  Lana
// Created: 14 April 2020 20:43:24
// Purpose: Definition of Class Ingredient

using System;

namespace Model.Medication
{
    public class Ingredient
    {
        private String name;
        private bool isAllergen;

        public Ingredient(string name, bool isAllergen)
        {
            this.name = name;
            this.isAllergen = isAllergen;
        }

        public Ingredient()
        {
        }

        public string Name { get => name; set => name = value; }
        public bool IsAllergen { get => isAllergen; set => isAllergen = value; }
    }
}