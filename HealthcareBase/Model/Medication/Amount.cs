// File:    Amount.cs
// Author:  Lana
// Created: 14 April 2020 20:49:40
// Purpose: Definition of Class Amount

namespace Model.Medication
{
    public class Amount
    {
        public Amount(double value, string unit, Ingredient ingredients)
        {
            Value = value;
            Unit = unit;
            Ingredients = ingredients;
        }

        public Amount()
        {
            Ingredients = new Ingredient();
        }

        public double Value { get; set; }

        public string Unit { get; set; }

        public Ingredient Ingredients { get; set; }
    }
}