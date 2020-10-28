// File:    Amount.cs
// Author:  Lana
// Created: 14 April 2020 20:49:40
// Purpose: Definition of Class Amount

using System;

namespace Model.Medication
{
    public class Amount
    {
        private double value;
        private String unit;
        private Ingredient ingredient;

        public Amount(double value, string unit, Ingredient ingredients)
        {
            this.value = value;
            this.unit = unit;
            this.ingredient = ingredients;
        }

        public Amount()
        {
            ingredient = new Ingredient();
        }

        public double Value { get => value; set => this.value = value; }
        public string Unit { get => unit; set => unit = value; }
        public Ingredient Ingredients { get => ingredient; set => ingredient = value; }
    }
}